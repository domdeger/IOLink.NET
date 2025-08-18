using FluentAssertions;
using IOLink.NET.Conversion;
using IOLink.NET.Integration;
using IOLink.NET.IODD.Provider;
using IOLink.NET.IODD.Resolution.Common;
using IOLink.NET.Vendors.Ifm;
using IOLink.NET.Visualization.Menu;
using IOLink.NET.Visualization.Structure.Structure;
using Vendors.Ifm.Configuration;

namespace Vendors.Ifm;

[Trait("Category", "IntegrationTest")]
[Collection("IfmIoTCoreIntegrationTest")]
public class IoTCoreIntegrationTest
{
    private readonly string _baseUrl = $"http://{MasterConfiguration.IP}/";

    [Fact]
    public async Task ShouldConvertProcessDataAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var definitionProvider = new DeviceDefinitionProvider(new IODDFinderPublicClient());
        var portReader = new IODDPortReader(
            masterConnection,
            definitionProvider,
            new IoddConverter(),
            new DefaultTypeResolverFactory()
        );

        await portReader.InitializeForPortAsync(3);

        var result = await portReader.ReadConvertedProcessDataInAsync();
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldConvertParameterDataAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var definitionProvider = new DeviceDefinitionProvider(new IODDFinderPublicClient());
        var portReader = new IODDPortReader(
            masterConnection,
            definitionProvider,
            new IoddConverter(),
            new DefaultTypeResolverFactory()
        );

        await portReader.InitializeForPortAsync(3);

        var result500 = await portReader.ReadConvertedParameterAsync(561, 0);
        result500.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldReadMenuValues()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var definitionProvider = new DeviceDefinitionProvider(new IODDFinderPublicClient());
        var portReader = new IODDPortReader(
            masterConnection,
            definitionProvider,
            new IoddConverter(),
            new DefaultTypeResolverFactory()
        );

        await portReader.InitializeForPortAsync(3);

        var V_SP_FH1 = await portReader.ReadConvertedParameterAsync(583, 0);
        var rP_FL1 = await portReader.ReadConvertedParameterAsync(584, 0);
        var dS1 = await portReader.ReadConvertedParameterAsync(581, 0);
        var dr1 = await portReader.ReadConvertedParameterAsync(582, 0);

        V_SP_FH1.Should().Be(600);
        rP_FL1.Should().Be(500);
        dS1.Should().Be(0);
        dr1.Should().Be(0);
    }

    [Fact]
    public async Task ShouldReadAllMenuValuesWithMenuDataReader()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var definitionProvider = new DeviceDefinitionProvider(new IODDFinderPublicClient());
        var portReader = new IODDPortReader(
            masterConnection,
            definitionProvider,
            new IoddConverter(),
            new DefaultTypeResolverFactory()
        );
        var menuDataReader = new MenuDataReader(portReader);

        await portReader.InitializeForPortAsync(3);
        await menuDataReader.InitializeForPortAsync(3);

        var readableMenus = menuDataReader.GetReadableMenus();
        await readableMenus.ReadAsync();

        readableMenus.ObserverRoleMenu.IdentificationMenu.Should().NotBeNull();
        readableMenus.MaintenanceRoleMenu.IdentificationMenu.Should().NotBeNull();
        readableMenus.SpecialistRoleMenu.IdentificationMenu.Should().NotBeNull();

        TestMenuSetIdentificationMenu(readableMenus.ObserverRoleMenu);
        TestMenuSetIdentificationMenu(readableMenus.MaintenanceRoleMenu);
        TestMenuSetIdentificationMenu(readableMenus.SpecialistRoleMenu);
    }

    private static void TestMenuSetIdentificationMenu(MenuSet menuSet)
    {
        var identificationMenuVariables = menuSet.IdentificationMenu.Variables?.ToList();
        var V_VendorName = identificationMenuVariables?[0];
        V_VendorName.Should().NotBeNull();
        V_VendorName?.VariableId.Should().Be("V_VendorName");
        V_VendorName?.Variable?.Id.Should().Be("V_VendorName");
        V_VendorName?.Value.Should().Be("ifm electronic gmbh");

        var V_ProductName = identificationMenuVariables?[1];
        V_ProductName.Should().NotBeNull();
        V_ProductName?.VariableId.Should().Be("V_ProductName");
        V_ProductName?.Variable?.Id.Should().Be("V_ProductName");
        V_ProductName?.Value.Should().Be("TV7105");

        var V_ProductText = identificationMenuVariables?[2];
        V_ProductText.Should().NotBeNull();
        V_ProductText?.VariableId.Should().Be("V_ProductText");
        V_ProductText?.Variable?.Id.Should().Be("V_ProductText");
        V_ProductText?.Value.Should().Be("Electronic Temperature Sensor");

        var V_SerialNumber = identificationMenuVariables?[3];
        V_SerialNumber.Should().NotBeNull();
        V_SerialNumber?.VariableId.Should().Be("V_SerialNumber");
        V_SerialNumber?.Variable?.Id.Should().Be("V_SerialNumber");
        V_SerialNumber?.Value.Should().Be("100001845450");

        var V_HardwareRevision = identificationMenuVariables?[4];
        V_HardwareRevision.Should().NotBeNull();
        V_HardwareRevision?.VariableId.Should().Be("V_HardwareRevision");
        V_HardwareRevision?.Variable?.Id.Should().Be("V_HardwareRevision");
        V_HardwareRevision?.Value.Should().Be("AE");

        var V_FirmwareRevision = identificationMenuVariables?[5];
        V_FirmwareRevision.Should().NotBeNull();
        V_FirmwareRevision?.VariableId.Should().Be("V_FirmwareRevision");
        V_FirmwareRevision?.Variable?.Id.Should().Be("V_FirmwareRevision");
        V_FirmwareRevision?.Value.Should().Be("106  ");

        var V_ApplicationSpecificTag = identificationMenuVariables?[6];
        V_ApplicationSpecificTag.Should().NotBeNull();
        V_ApplicationSpecificTag?.VariableId.Should().Be("V_ApplicationSpecificTag");
        V_ApplicationSpecificTag?.Variable?.Id.Should().Be("V_ApplicationSpecificTag");
        V_ApplicationSpecificTag
            ?.Value.Should()
            .Be("***\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0");
    }
}
