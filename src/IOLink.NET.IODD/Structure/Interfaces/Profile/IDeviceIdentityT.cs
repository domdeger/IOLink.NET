using IOLink.NET.IODD.Structure.Common;

namespace IOLink.NET.IODD.Structure.Interfaces.Profile;
public interface IDeviceIdentityT
{
    ushort VendorId { get; }
    uint DeviceId { get; }
    string VendorName { get; }
    TextRefT VendorText { get; }
    TextRefT VendorUrl { get; }
    TextRefT DeviceName { get; }
    TextRefT DeviceFamily { get; }
}
