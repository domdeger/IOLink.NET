using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.Integration;
using IOLinkNET.IODD.Provider;
using IOLinkNET.IODD.Resolution.Common;
using IOLinkNET.Vendors.Ifm;
using IOLinkNET.Visualization.Menu;

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
        var portReader = new IODDPortReader(masterConnection, definitionProvider, new IoddConverter(), new DefaultTypeResolverFactory());

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
        var portReader = new IODDPortReader(masterConnection, definitionProvider, new IoddConverter(), new DefaultTypeResolverFactory());

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
        var portReader = new IODDPortReader(masterConnection, definitionProvider, new IoddConverter(), new DefaultTypeResolverFactory());
        var menuDataReader = new MenuDataReader(portReader);

        await portReader.InitializeForPortAsync(3);
        await menuDataReader.InitializeForPortAsync(3);

        var readableMenus = menuDataReader.GetReadableMenus();
        await readableMenus.ReadAsync();
        /*await readableMenus.ReadAsync();
        readableMenus.Should().NotBeNull();*/

        /*var V_SP_FH1 = await portReader.ReadConvertedParameterAsync(583, 0);
        var rP_FL1 = await portReader.ReadConvertedParameterAsync(584, 0);
        var dS1 = await portReader.ReadConvertedParameterAsync(581, 0);
        var dr1 = await portReader.ReadConvertedParameterAsync(582, 0);

        V_SP_FH1.Should().Be(600);
        rP_FL1.Should().Be(500);
        dS1.Should().Be(0);
        dr1.Should().Be(0);*/
    }
}