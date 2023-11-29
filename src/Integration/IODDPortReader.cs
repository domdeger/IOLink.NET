using IOLinkNET.Device.Contract;
using IOLinkNET.IODD.Provider;
using IOLinkNET.IODD.Structure;

namespace IOLinkNET.Integration;

public class IODDPortReader
{
    private readonly IMasterConnection _connection;
    private readonly IDeviceDefinitionProvider _deviceDefinitionProvider;

    private int? activePdCondition;
    private IODevice? _deviceDefinition;

    public IODDPortReader(IMasterConnection connection, IDeviceDefinitionProvider deviceDefinitionProvider)
    {
        _connection = connection;
        _deviceDefinitionProvider = deviceDefinitionProvider;
    }

    public async Task InitializeForPortAsync(byte port)
    {
        var portInfo = await _connection.GetPortInformationAsync(port);
        if (portInfo.Status != PortStatus.IOLink)
        {
            throw new InvalidOperationException("Port is not in IO-Link mode");
        }

        if (portInfo.DeviceInformation is null)
        {
            throw new InvalidOperationException($"Device information is not available for requested port {port}");
        }

        _deviceDefinition = await _deviceDefinitionProvider.GetDeviceDefinitionAsync(portInfo.DeviceInformation.VendorId, portInfo.DeviceInformation.DeviceId, portInfo.DeviceInformation.ProductId);

        _deviceDefinition.
    }
}
