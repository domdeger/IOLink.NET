using Vendors.Ifm.Configuration;

namespace Vendors.Ifm;

[Trait("Category", "IntegrationTest")]
[Collection("IfmIoTCoreIntegrationTest")]
public class IfmIotCoreMasterConnectionTests
{
    private readonly string _baseUrl = $"http://{MasterConfiguration.IP}/";

    [Fact]
    public async Task CanGetPortCountAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var result = await masterConnection.GetPortCountAsync(CancellationToken.None);
        result.ShouldBe((byte)4);
    }

    [Fact]
    public async Task CanIdentifyPortAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var result = await masterConnection.GetPortInformationAsync(3, CancellationToken.None);
        result.ShouldNotBeNull();
    }

    [Fact]
    public async Task CanGetPortInformationsAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var result = await masterConnection.GetPortInformationsAsync(CancellationToken.None);
        result.ShouldNotBeNull();
        result.Length.ShouldBe(4);
    }
}
