namespace IOLinkNET.IODD.Provider;

public interface IIODDProvider
{
    Task<Stream> GetDeviceAsync(ushort vendorId, uint deviceId, string productId, CancellationToken cancellationToken = default);
}