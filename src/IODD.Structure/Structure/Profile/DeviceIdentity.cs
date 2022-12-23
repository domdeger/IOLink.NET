using IOLinkNET.IODD.Structure.Common;

namespace IODD.Structure.Structure.Profile
{
    public record DeviceIdentityT(ushort VendorId, uint DeviceId, string VendorName, TextRefT VendorText, TextRefT VendorUrl, TextRefT DeviceName, TextRefT DeviceFamily);
}