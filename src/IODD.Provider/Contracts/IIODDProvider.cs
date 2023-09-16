namespace IOLinkNET.IODD.Provider;

public interface IIODDProvider
{
    Task<Stream> GetIODDPackageAsync(ushort vendorId, uint deviceId, string productId, CancellationToken cancellationToken = default);
}