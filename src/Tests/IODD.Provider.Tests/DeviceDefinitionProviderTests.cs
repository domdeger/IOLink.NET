using IOLinkNET.IODD.Provider;

namespace IODD.Provider.Tests;

public class DeviceDefinitionProviderTests
{
    private readonly Uri _baseUrl = new("https://ioddfinder.io-link.com/");

    [Fact]
    public async Task DoesLoadDeviceDefinitionAsync()
    {
        var client = new IODDFinderPublicClient(_baseUrl);
        var provider = new DeviceDefinitionProvider(client);
        var definition = await provider.GetDeviceDefinitionAsync(888, 200710, "50142212");

        definition.ProfileBody.DeviceIdentity.VendorId.Should().Be(888);
    }

    [Fact]
    public async Task DoesLoadDeviceDefinitionForIfmDeviceAsync()
    {
        var client = new IODDFinderPublicClient(_baseUrl);
        var provider = new DeviceDefinitionProvider(client);
        var definition = await provider.GetDeviceDefinitionAsync(310, 1367, "VVB001 Status B");

        definition.ProfileBody.DeviceIdentity.VendorId.Should().Be(310);
        definition.ProfileBody.DeviceIdentity.DeviceId.Should().Be(1367);
    }
}
