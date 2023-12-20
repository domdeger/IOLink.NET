namespace IOLinkNET.Vendors.Ifm;

public static class IfmIoTCoreMasterConnectionFactory
{
    public static IfmIotCoreMasterConnection Create(string baseUrl)
    {
        var iotCoreClient = IfmIoTCoreClientFactory.Create(baseUrl);
        return new IfmIotCoreMasterConnection(iotCoreClient);
    }
}