using IOLink.NET.Conversion;
using IOLink.NET.Integration;
using IOLink.NET.IODD.Provider;
using IOLink.NET.IODD.Resolution.Common;
using IOLink.NET.Vendors.Ifm;
using IOLink.NET.Visualization.Menu;
using IOLink.NET.Visualization.Structure.Structure;
using Shouldly;
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
        result.ShouldNotBeNull();
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
        result500.ShouldNotBeNull();
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

        V_SP_FH1.ShouldBe(600);
        rP_FL1.ShouldBe(500);
        dS1.ShouldBe(0);
        dr1.ShouldBe(0);
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

        readableMenus.ObserverRoleMenu.IdentificationMenu.ShouldNotBeNull();
        readableMenus.MaintenanceRoleMenu.IdentificationMenu.ShouldNotBeNull();
        readableMenus.SpecialistRoleMenu.IdentificationMenu.ShouldNotBeNull();

        TestMenuSetIdentificationMenu(readableMenus.ObserverRoleMenu);
        TestMenuSetIdentificationMenu(readableMenus.MaintenanceRoleMenu);
        TestMenuSetIdentificationMenu(readableMenus.SpecialistRoleMenu);
    }

    private static void TestMenuSetIdentificationMenu(MenuSet menuSet)
    {
        var identificationMenuVariables = menuSet.IdentificationMenu.Variables?.ToList();
        var V_VendorName = identificationMenuVariables?[0];
        V_VendorName.ShouldNotBeNull();
        V_VendorName?.VariableId.ShouldBe("V_VendorName");
        V_VendorName?.Variable?.Id.ShouldBe("V_VendorName");
        V_VendorName?.Value.ShouldBe("ifm electronic gmbh");

        var V_ProductName = identificationMenuVariables?[1];
        V_ProductName.ShouldNotBeNull();
        V_ProductName?.VariableId.ShouldBe("V_ProductName");
        V_ProductName?.Variable?.Id.ShouldBe("V_ProductName");
        V_ProductName?.Value.ShouldBe("TV7105");

        var V_ProductText = identificationMenuVariables?[2];
        V_ProductText.ShouldNotBeNull();
        V_ProductText?.VariableId.ShouldBe("V_ProductText");
        V_ProductText?.Variable?.Id.ShouldBe("V_ProductText");
        V_ProductText?.Value.ShouldBe("Electronic Temperature Sensor");

        var V_SerialNumber = identificationMenuVariables?[3];
        V_SerialNumber.ShouldNotBeNull();
        V_SerialNumber?.VariableId.ShouldBe("V_SerialNumber");
        V_SerialNumber?.Variable?.Id.ShouldBe("V_SerialNumber");
        V_SerialNumber?.Value.ShouldBe("100001845450");

        var V_HardwareRevision = identificationMenuVariables?[4];
        V_HardwareRevision.ShouldNotBeNull();
        V_HardwareRevision?.VariableId.ShouldBe("V_HardwareRevision");
        V_HardwareRevision?.Variable?.Id.ShouldBe("V_HardwareRevision");
        V_HardwareRevision?.Value.ShouldBe("AE");

        var V_FirmwareRevision = identificationMenuVariables?[5];
        V_FirmwareRevision.ShouldNotBeNull();
        V_FirmwareRevision?.VariableId.ShouldBe("V_FirmwareRevision");
        V_FirmwareRevision?.Variable?.Id.ShouldBe("V_FirmwareRevision");
        V_FirmwareRevision?.Value.ShouldBe("106  ");

        var V_ApplicationSpecificTag = identificationMenuVariables?[6];
        V_ApplicationSpecificTag.ShouldNotBeNull();
        V_ApplicationSpecificTag?.VariableId.ShouldBe("V_ApplicationSpecificTag");
        V_ApplicationSpecificTag?.Variable?.Id.ShouldBe("V_ApplicationSpecificTag");
        V_ApplicationSpecificTag
            ?.Value?.ToString()
            .ShouldBe<string>("***\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0");
    }
}
