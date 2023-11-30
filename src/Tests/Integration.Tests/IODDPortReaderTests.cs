using System.Xml.Linq;

using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.Device.Contract;
using IOLinkNET.Integration;
using IOLinkNET.IODD;
using IOLinkNET.IODD.Provider;

using NSubstitute;

namespace Integration.Tests;

public class IODDPortReaderTests
{

    [Theory]
    [InlineData(888, 328205, "BNI IOL-727-S51-P012", "Balluff", "TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml")]
    [InlineData(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml")]
    public async Task CanInitializeForPortAsync(ushort vendorId, uint deviceId, string productId, string vendorName, string ioddPath)
    {
        var (portReader, ioddProvider, masterConnection) = PreparePortReader(vendorId, deviceId, productId, vendorName, ioddPath);

        await portReader.InitializeForPortAsync(1);

        await ioddProvider.Received().GetDeviceDefinitionAsync(vendorId, deviceId, productId, Arg.Any<CancellationToken>());
        await masterConnection.Received().GetPortInformationAsync(1, Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml")]
    public async Task CanReadConvertedParameterAsync(ushort vendorId, uint deviceId, string productId, string vendorName, string ioddPath)
    {
        var (portReader, _, masterConnection) = PreparePortReader(vendorId, deviceId, productId, vendorName, ioddPath);
        masterConnection.ReadIndexAsync(1, 58).Returns(new byte[] { 0x00, 0x00, 0x00, 0x04 });
        await portReader.InitializeForPortAsync(1);

        var converted = await portReader.ReadConvertedParameter(58, 0);
        converted.Should().Be(4);
    }

    private (IODDPortReader, IDeviceDefinitionProvider, IMasterConnection) PreparePortReader(ushort vendorId, uint deviceId, string productId, string vendorName, string ioddPath)
    {
        var ioddParser = new IODDParser();
        var device = ioddParser.Parse(XElement.Load(ioddPath));
        var ioddProvider = Substitute.For<IDeviceDefinitionProvider>();
        ioddProvider.GetDeviceDefinitionAsync(vendorId, deviceId, productId, Arg.Any<CancellationToken>())
            .Returns(device);

        var masterConnection = GetMasterConnectionMock(vendorId, deviceId, productId, vendorName);

        var portReader = new IODDPortReader(masterConnection, ioddProvider, new IoddConverter());

        return (portReader, ioddProvider, masterConnection);
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