using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.Profile;

public record DeviceIdentityT(ushort VendorId, uint DeviceId, string VendorName, TextRefT VendorText, TextRefT VendorUrl, TextRefT DeviceName, TextRefT DeviceFamily);