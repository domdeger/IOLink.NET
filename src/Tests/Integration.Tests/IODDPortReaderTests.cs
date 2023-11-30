using System.Xml.Linq;

using IOLinkNET.Conversion;
using IOLinkNET.Device.Contract;
using IOLinkNET.Integration;
using IOLinkNET.IODD;
using IOLinkNET.IODD.Provider;

using NSubstitute;

namespace Integration.Tests;

public class IODDPortReaderTests
{

    [Fact]
    public async Task CanInitializeForPort()
    {
        ushort vendorId = 888;
        uint deviceId = 328205;
        var productId = "BNI IOL-727-S51-P012";

        var ioddParser = new IODDParser();
        var device = ioddParser.Parse(XElement.Load("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml"));
        var ioddProvider = Substitute.For<IDeviceDefinitionProvider>();
        ioddProvider.GetDeviceDefinitionAsync(vendorId, deviceId, productId, Arg.Any<CancellationToken>())
            .Returns(device);

        var masterConnection = GetMasterConnectionMock(vendorId, deviceId, productId, "Balluff");

        var portReader = new IODDPortReader(masterConnection, ioddProvider, new IoddConverter());

        await portReader.InitializeForPortAsync(1);
    }

    private IMasterConnection GetMasterConnectionMock(ushort vendorId, uint deviceId, string productId, string vendorName)
    {
        var portInfo = Substitute.For<IPortInformation>();
        var deviceInfo = Substitute.For<IDeviceInformation>();
        deviceInfo.VendorId.Returns(vendorId);
        deviceInfo.DeviceId.Returns(deviceId);
        deviceInfo.ProductId.Returns(productId);
        deviceInfo.VendorName.Returns(vendorName);

        portInfo.PortNumber.Returns((byte)1);
        portInfo.Status.Returns(PortStatus.Connected | PortStatus.IOLink);
        portInfo.DeviceInformation.Returns(deviceInfo);

        var masterConnection = Substitute.For<IMasterConnection>();
        masterConnection.GetPortInformationAsync(1, Arg.Any<CancellationToken>())
            .Returns(portInfo);

        return masterConnection;
    }
}