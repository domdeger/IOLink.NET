using FluentAssertions;

using IOLinkNET.Vendors.Ifm;

[Trait("Category", "IntegrationTest")]
public class IfmIotCoreMasterConnectionTests
{

    private readonly string _baseUrl = "http://192.168.2.227/";


    [Fact]
    public async Task CanIdentifyPortAsync()
    {
        var masterClient = IfmIoTCoreClientFactory.Create(_baseUrl);
        var masterConnection = new IfmIotCoreMasterConnection(masterClient);

        var result = await masterConnection.GetPortInformationAsync(3);
        result.Should().NotBeNull();
    }
}