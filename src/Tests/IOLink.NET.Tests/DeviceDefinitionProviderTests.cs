using IOLink.NET.IODD.Provider;

namespace IOLink.NET.Tests;

public class DeviceDefinitionProviderTests
{
    private readonly Uri _baseUrl = new("https://ioddfinder.io-link.com/");

    [Fact]
    public async Task DoesLoadDeviceDefinitionAsync()
    {
        var client = new IODDFinderPublicClient(_baseUrl);
        var provider = new DeviceDefinitionProvider(client);
        var definition = await provider.GetDeviceDefinitionAsync(
            888,
            200710,
            "50142212",
            CancellationToken.None
        );

        definition.ProfileBody.DeviceIdentity.VendorId.ShouldBe((ushort)888);
    }
}
