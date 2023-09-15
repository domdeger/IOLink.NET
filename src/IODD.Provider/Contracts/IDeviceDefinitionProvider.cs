using IOLinkNET.IODD.Structure;

namespace IOLinkNET.IODD.Provider;

public interface IDeviceDefinitionProvider
{
    Task<IODevice> GetDeviceDefinitionAsync(ushort vendorId, uint deviceId, string productId, CancellationToken cancellationToken = default);
}