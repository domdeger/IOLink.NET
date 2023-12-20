using System.Xml.Linq;

using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.Device.Contract;
using IOLinkNET.Integration;
using IOLinkNET.IODD;
using IOLinkNET.IODD.Provider;
using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Resolution.Contracts;
using IOLinkNET.IODD.Structure;

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

    [Fact]
    public async Task ShouldThrowIfPortIsNotInIOLinkModeAsync()
    {
        var (portReader, _, masterConnection) = PreparePortReader(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml");
        var portInfo = Substitute.For<IPortInformation>();
        portInfo.Status.Returns(PortStatus.Connected);
        masterConnection.GetPortInformationAsync(1, Arg.Any<CancellationToken>()).Returns(portInfo);

        var initTask = () => portReader.InitializeForPortAsync(1);

        await initTask.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task ShouldThrowIfNoDeviceInfoAsync()
    {
        var (portReader, _, masterConnection) = PreparePortReader(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml");
        var portInfo = Substitute.For<IPortInformation>();
        portInfo.Status.Returns(PortStatus.Connected | PortStatus.IOLink);
        portInfo.DeviceInformation.Returns(null as IDeviceInformation);

        masterConnection.GetPortInformationAsync(1, Arg.Any<CancellationToken>()).Returns(portInfo);

        var initTask = () => portReader.InitializeForPortAsync(1);

        await initTask.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task ShouldThrowIfUninitializedParameterReadAsync()
    {
        var (portReader, _, _) = PreparePortReader(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml");
        var readParamTask = () => portReader.ReadConvertedParameterAsync(58, 0);

        await readParamTask.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task ShouldThrowIfUninitializedProcessDataInReadAsync()
    {
        var (portReader, _, _) = PreparePortReader(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml");
        var readParamTask = portReader.ReadConvertedProcessDataInAsync;

        await readParamTask.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task ShouldThrowIfUninitializedProcessDataOutReadAsync()
    {
        var (portReader, _, _) = PreparePortReader(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml");
        var readParamTask = portReader.ReadConvertedProcessDataOutAsync;

        await readParamTask.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task CanReadConvertedProcessDataInWithConditionAsync()
    {
        var (portReader, _, masterConnection) = PreparePortReader(1222, 18, "CSS 01411.2-xx", "STEGO", "TestData/STEGO-SmartSensor-CSS014-08-20190726-IODD1.1.xml");
        masterConnection.ReadIndexAsync(1, 66).Returns(new byte[] { 0x00 });
        masterConnection.ReadProcessDataInAsync(1).Returns(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x0, 0x0, 0x0, 0x0 });
        await portReader.InitializeForPortAsync(1);

        var pd = (await portReader.ReadConvertedProcessDataInAsync()) as IEnumerable<(string, object)>;
        pd.Should().ContainEquivalentOf(("TN_PDI_Feuchte", 0));
    }

    [Theory]
    [InlineData(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml")]
    public async Task CanReadConvertedParameterAsync(ushort vendorId, uint deviceId, string productId, string vendorName, string ioddPath)
    {
        var (portReader, _, masterConnection) = PreparePortReader(vendorId, deviceId, productId, vendorName, ioddPath);
        masterConnection.ReadIndexAsync(1, 58).Returns(new byte[] { 0x00, 0x00, 0x00, 0x04 });
        await portReader.InitializeForPortAsync(1);

        var converted = await portReader.ReadConvertedParameterAsync(58, 0);
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
        var typeResolverFactory = Substitute.For<ITypeResolverFactory>();
        typeResolverFactory.CreateParameterTypeResolver(Arg.Any<IODevice>()).Returns(d => new ParameterTypeResolver(d.Arg<IODevice>()));
        typeResolverFactory.CreateProcessDataTypeResolver(Arg.Any<IODevice>()).Returns(d => new ProcessDataTypeResolver(d.Arg<IODevice>()));

        var portReader = new IODDPortReader(masterConnection, ioddProvider, new IoddConverter(), typeResolverFactory);

        return (portReader, ioddProvider, masterConnection);
    }

    private IMasterConnection GetMasterConnectionMock(ushort vendorId, uint deviceId, string productId, string vendorName)
    {
        var portInfo = Substitute.For<IPortInformation>();
        var deviceInfo = Substitute.For<IDeviceInformation>();
        deviceInfo.VendorId.Returns(vendorId);
        deviceInfo.DeviceId.Returns(deviceId);
        deviceInfo.ProductId.Returns(productId);

        portInfo.PortNumber.Returns((byte)1);
        portInfo.Status.Returns(PortStatus.Connected | PortStatus.IOLink);
        portInfo.DeviceInformation.Returns(deviceInfo);

        var masterConnection = Substitute.For<IMasterConnection>();
        masterConnection.GetPortInformationAsync(1, Arg.Any<CancellationToken>())
            .Returns(portInfo);

        return masterConnection;
    }
}