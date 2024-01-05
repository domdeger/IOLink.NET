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
using IOLinkNET.Visualization.Menu;

using NSubstitute;

namespace Visualization.Tests;

public class MenuDataReaderTests
{
    [Fact]
    public void CanInitializeMenuDataReader()
    {
        var menuDataReader = new MenuDataReader(GetSubstituteForIODDPortReader());
        menuDataReader.Should().NotBeNull();
    }

    [Fact]
    public void GetIODDRawMenuStructure_ShouldThrowIfNotInitialized()
    {
        var menuDataReaderAction = () => new MenuDataReader(GetSubstituteForIODDPortReader()).GetIODDRawMenuStructure();

        menuDataReaderAction.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GetReadableMenus_ShouldThrowIfNotInitialized()
    {
        var menuDataReaderAction = () => new MenuDataReader(GetSubstituteForIODDPortReader()).GetReadableMenus();
        
        menuDataReaderAction.Should().Throw<InvalidOperationException>();
    }


    [Theory]
    [InlineData(310, 733, "TV7105", "ifm electronic gmbh ", "TestData/ifm-0002DD-20230324-IODD1.1.xml")]
    [InlineData(888, 328205, "BNI IOL-727-S51-P012", "Balluff", "TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml")]
    [InlineData(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml")]
    /*[InlineData(1222, 18, "CSS 01411.2-xx", "STEGO Elektrotechnik GmbH", "TestData/STEGO-SmartSensor-CSS014-08-20190726-IODD1.1.xml")]*/
    public async Task CanReadMenus(ushort vendorId, uint deviceId, string productId, string vendorName, string ioddPath)
    {
        var (_, _, masterConnection, menuDataReader) = PreparePortReader(vendorId, deviceId, productId, vendorName, ioddPath);
        masterConnection.ReadProcessDataInAsync(1).Returns(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x0, 0x0, 0x0, 0x0 });
        await menuDataReader.InitializeForPortAsync(1);
        var readableMenus = menuDataReader.GetReadableMenus();
        await readableMenus.ReadAsync();

        readableMenus.Should().NotBeNull();
    }

    [Theory]
    [InlineData(310, 733, "TV7105", "ifm electronic gmbh ", "TestData/ifm-0002DD-20230324-IODD1.1.xml", 46)]
    [InlineData(888, 328205, "BNI IOL-727-S51-P012", "Balluff", "TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml", 84)]
    [InlineData(888, 459267, "BCS012N", "Balluff", "TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml", 8)]
    public async Task ProvidesRawIoddMenuStructure(ushort vendorId, uint deviceId, string productId, string vendorName, string ioddPath, int menuCollectionCount)
    {
        var (_, _, _, menuDataReader) = PreparePortReader(vendorId, deviceId, productId, vendorName, ioddPath);
        await menuDataReader.InitializeForPortAsync(1);
        var rawMenuStructure = menuDataReader.GetIODDRawMenuStructure();

        rawMenuStructure.Should().NotBeNull();

        rawMenuStructure.MaintenanceRoleMenuSet.IdentificationMenu.Should().NotBeNull();
        rawMenuStructure.ObserverRoleMenuSet.IdentificationMenu.Should().NotBeNull();
        rawMenuStructure.SpecialistRoleMenuSet.IdentificationMenu.Should().NotBeNull();

        rawMenuStructure.MenuCollection.Count().Should().Be(menuCollectionCount);
    }

    private static IODDPortReader GetSubstituteForIODDPortReader()
    {
        return Substitute.For<IODDPortReader>(Substitute.For<IMasterConnection>(), Substitute.For<IDeviceDefinitionProvider>(), new IoddConverter(), Substitute.For<ITypeResolverFactory>());
    }

    private (IODDPortReader, IDeviceDefinitionProvider, IMasterConnection, MenuDataReader) PreparePortReader(ushort vendorId, uint deviceId, string productId, string vendorName, string ioddPath)
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

        var portReader = Substitute.For<IODDPortReader>(masterConnection, ioddProvider, new IoddConverter(), typeResolverFactory);
        var menuDataReader = new MenuDataReader(portReader);

        portReader.ReadConvertedParameterAsync(Arg.Any<ushort>(), Arg.Any<byte>()).Returns(string.Empty);

        return (portReader, ioddProvider, masterConnection, menuDataReader);
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

        masterConnection.ReadIndexAsync(portInfo.PortNumber, Arg.Any<ushort>()).Returns((byte[])[0]);

        return masterConnection;
    }
}