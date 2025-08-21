namespace IOLink.NET.Core.Contracts;

public interface IIODDProvider
{
    Task<Stream> GetIODDPackageAsync(
        ushort vendorId,
        uint deviceId,
        string productId,
        CancellationToken cancellationToken
    );
}
