using IODD.Provider;

using IOLinkNET.IODD.Provider;

namespace IODD.Provider.Tests;

public class IODDFinderPublicClientTests
{
    private readonly Uri _baseUrl = new("https://ioddfinder.io-link.com/");
    [Fact]
    public async Task DoesLoadIoddAsync()
    {
        var client = new IODDFinderPublicClient(_baseUrl);
        var iodd = await client.GetIODDPackageAsync(888, 131329, "");

        iodd.Should().NotBeNull();
        iodd.CanRead.Should().BeTrue();
    }
}