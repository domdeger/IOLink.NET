using IOLinkNET.Device.Contract;

namespace IOLinkNET.Device.Model;

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