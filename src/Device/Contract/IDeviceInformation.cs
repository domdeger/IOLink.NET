namespace IOLinkNET.Device.Contract;

public interface IDeviceInformation
{
    public ushort VendorId { get; }

    public uint DeviceId { get; }

    public string VendorName { get; }

    public string ProductId { get; }

}