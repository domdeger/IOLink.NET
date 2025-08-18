using IOLink.NET.Core.Contracts;

namespace IOLink.NET.Core.Models;

public class DeviceInformation : IDeviceInformation
{
    public DeviceInformation(ushort VendorId, uint DeviceId, string ProductId)
    {
        this.VendorId = VendorId;
        this.DeviceId = DeviceId;
        this.ProductId = ProductId;
    }

    public ushort VendorId { get; }

    public uint DeviceId { get; }

    public string ProductId { get; }
}
