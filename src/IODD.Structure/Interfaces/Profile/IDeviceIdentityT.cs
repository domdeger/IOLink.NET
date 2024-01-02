using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.Interfaces.Profile;
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
