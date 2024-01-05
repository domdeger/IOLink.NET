using System.Text.Json;

using IOLinkNET.Device.Contract;
using IOLinkNET.Device.Model;
using IOLinkNET.Vendors.Ifm.Data;

namespace IOLinkNET.Vendors.Ifm;

public class IfmIotCoreMasterConnection : IMasterConnection
{
    private readonly IIfmIoTCoreClient _client;

    public IfmIotCoreMasterConnection(IIfmIoTCoreClient client)
    {
        _client = client;
    }

    public async Task<byte> GetPortCountAsync(CancellationToken cancellationToken = default)
    {
        var portTree = await _client.GetPortTreeAsync(new IfmIoTGetPortTreeRequest(), cancellationToken);

        if (portTree.Data.Subs == null)
        {
            throw new Exception("PortTree is null");
        }

        return (byte)portTree.Data.Subs.Count();
    }
    public async Task<IPortInformation> GetPortInformationAsync(byte portNumber, CancellationToken cancellationToken = default)
    {
        var statusPath = IfmIoTCoreServicePathBuilder.PortDeviceStatusPath(portNumber);
        var productNamePath = IfmIoTCoreServicePathBuilder.PortDeviceProductNamePath(portNumber);
        var vendorIdPath = IfmIoTCoreServicePathBuilder.PortDeviceVendorIdPath(portNumber);
        var deviceIdPath = IfmIoTCoreServicePathBuilder.PortDeviceIdPath(portNumber);
        var masterCycleTimeActualPath = IfmIoTCoreServicePathBuilder.PortMasterCycleTimeActualPath(portNumber);
        var modePath = IfmIoTCoreServicePathBuilder.PortModePath(portNumber);
        var comSpeedPath = IfmIoTCoreServicePathBuilder.PortComSpeedPath(portNumber);


        var resp = await _client.GetDataMultiAsync(new IfmIoTGetDataMultiRequest(new[] {
                                        statusPath, productNamePath, vendorIdPath, deviceIdPath,
                                        masterCycleTimeActualPath, modePath, comSpeedPath }), cancellationToken);

        var mode = resp.Data[modePath].Data.Deserialize<IfmIotCorePortMode>();
        var status = resp.Data[statusPath].Data.Deserialize<IfmIoTCorePortStatus>();
        if (status == IfmIoTCorePortStatus.NotConnected)
        {
            return new PortInformation(portNumber, PortStatus.Disconnected, null);
        }

        var comSpeed = resp.Data[comSpeedPath].Data.Deserialize<IfmIotCorePortComSpeed>();

        var deviceInfo = new DeviceInformation(resp.Data[vendorIdPath].Data.Deserialize<ushort>(),
                                               resp.Data[deviceIdPath].Data.Deserialize<uint>(),
                                               resp.Data[productNamePath].Data.Deserialize<string>()!
                                              );

        // ToDo: extract and complete this logic.
        var portStatus = status == IfmIoTCorePortStatus.NotConnected ? PortStatus.Disconnected : PortStatus.Connected;
        var iolstatus = mode == IfmIotCorePortMode.IOLink ? PortStatus.IOLink : PortStatus.DI;

        var portInfo = new PortInformation(portNumber, portStatus | iolstatus, deviceInfo);

        return portInfo;

    }

    public async Task<IPortInformation[]> GetPortInformationsAsync(CancellationToken cancellationToken = default)
    {
        var tasks = new List<Task<IPortInformation>>();
        for (byte i = 1; i <= await GetPortCountAsync(); i++)
        {
            tasks.Add(GetPortInformationAsync(i, cancellationToken));
        }
        return await Task.WhenAll(tasks);
    }

    public async Task<ReadOnlyMemory<byte>> ReadIndexAsync(byte portNumber, ushort index, byte subIndex = 0, CancellationToken cancellationToken = default)
    {
        var resp = await _client.GetDeviceAcyclicDataAsync(new IfmIoTReadAcyclicRequest(portNumber, index, subIndex), cancellationToken);
        if (resp?.Data == null)
        {
            return null;
        }

        return Convert.FromHexString(resp.Data.Value);
    }
    public async Task<ReadOnlyMemory<byte>> ReadProcessDataInAsync(byte portNumber, CancellationToken cancellationToken = default)
    {
        var resp = await _client.GetDevicePdinDataAsync(new IfmIoTReadPdInRequest(portNumber), cancellationToken);
        return Convert.FromHexString(resp.Data.Value);
    }
    public async Task<ReadOnlyMemory<byte>> ReadProcessDataOutAsync(byte portNumber, CancellationToken cancellationToken = default)
    {
        var resp = await _client.GetDevicePdoutDataAsync(new IfmIoTReadPdOutRequest(portNumber), cancellationToken);
        return Convert.FromHexString(resp.Data.Value);
    }
}