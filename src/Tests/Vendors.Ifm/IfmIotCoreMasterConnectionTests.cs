using FluentAssertions;

using IOLinkNET.Vendors.Ifm;

namespace Vendors.Ifm;

[Trait("Category", "IntegrationTest")]
[Collection("IfmIoTCoreIntegrationTest")]
public class IfmIotCoreMasterConnectionTests
{

    private readonly string _baseUrl = "http://192.168.2.207/";

    [Fact]
    public async Task CanGetPortCountAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var result = await masterConnection.GetPortCountAsync();
        result.Should().Be(4);
    }

    [Fact]
    public async Task CanIdentifyPortAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var result = await masterConnection.GetPortInformationAsync(3);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task CanGetPortInformationsAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var result = await masterConnection.GetPortInformationsAsync();
        result.Should().NotBeNull();
        result.Length.Should().Be(4);
    }
}