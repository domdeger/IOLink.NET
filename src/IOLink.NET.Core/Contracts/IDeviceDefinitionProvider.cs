namespace IOLink.NET.Core.Contracts;

// Forward declaration - IODevice will be defined in IOLink.NET.IODD
// This interface provides abstraction for device definition providers
public interface IDeviceDefinitionProvider<T>
{
    Task<T> GetDeviceDefinitionAsync(
        ushort vendorId,
        uint deviceId,
        string productId,
        CancellationToken cancellationToken
    );
}
