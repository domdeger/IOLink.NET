namespace IOLink.NET.Core.Contracts;

public interface IDeviceInformation
{
    public ushort VendorId { get; }

    public uint DeviceId { get; }

    public string ProductId { get; }
}
