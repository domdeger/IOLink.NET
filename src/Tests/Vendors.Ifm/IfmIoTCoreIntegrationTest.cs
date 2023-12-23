using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.Integration;
using IOLinkNET.IODD.Provider;
using IOLinkNET.IODD.Resolution.Common;
using IOLinkNET.Vendors.Ifm;

namespace Vendors.Ifm;

[Trait("Category", "IntegrationTest")]
[Collection("IfmIoTCoreIntegrationTest")]
public class IoTCoreIntegrationTest
{
    private readonly string _baseUrl = "http://192.168.2.207/";

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
}