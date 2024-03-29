using Refit;

namespace IOLinkNET.Vendors.Ifm;

public static class IfmIoTCoreClientFactory
{
    public static IIfmIoTCoreClient Create(string baseUrl)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        return RestService.For<IIfmIoTCoreClient>(httpClient);
    }
}