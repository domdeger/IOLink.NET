using IOLink.NET.IODD.Structure;

namespace IOLink.NET.IODD.Provider;

public interface IDeviceDefinitionProvider
{
    Task<IODevice> GetDeviceDefinitionAsync(
        ushort vendorId,
        uint deviceId,
        string productId,
        CancellationToken cancellationToken
    );
}
