using System.Net.Http;
using System.Net.Http.Json;
using IOLink.NET.IODD.Provider.Data;

namespace IOLink.NET.IODD.Provider;

public class IODDFinderPublicClient : IIODDProvider
{
    private readonly HttpClient _httpClient;

    public IODDFinderPublicClient(Uri baseUrl, bool shouldValidateSSLCertificate = true)
    {
        if (shouldValidateSSLCertificate)
        {
            _httpClient = new() { BaseAddress = baseUrl };
        }
        else
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                {
                    return true;
                },
            };
            _httpClient = new(handler) { BaseAddress = baseUrl };
        }
    }

    public IODDFinderPublicClient()
        : this(new Uri("https://ioddfinder.io-link.com/")) { }

    public async Task<Stream> GetIODDPackageAsync(
        ushort vendorId,
        uint deviceId,
        string productId,
        CancellationToken cancellationToken = default
    )
    {
        var entries = await _httpClient.GetFromJsonAsync<IODDFinderSearchResponse>(
            $"api/drivers?status=APPROVED&status=UPLOADED&vendorId={vendorId}&deviceId={deviceId}&productId={productId}"
        );
        if (entries is null)
        {
            throw new InvalidOperationException("Could not deserialize response");
        }

        if (entries.Content.Count() < 1)
        {
            entries = await _httpClient.GetFromJsonAsync<IODDFinderSearchResponse>(
                $"api/drivers?status=APPROVED&status=UPLOADED&vendorId={vendorId}&deviceId={deviceId}"
            );

            if (entries?.Content.Count() < 1)
            {
                throw new InvalidOperationException("No entries found");
            }
        }

        var entry = entries.Content.OrderByDescending(x => x.IoLinkRev).First();

        var zipStream = await _httpClient.GetStreamAsync(
            $"api/vendors/{vendorId}/iodds/{entry.IoddId}/files/zip/rated"
        );

        return zipStream;
    }
}
