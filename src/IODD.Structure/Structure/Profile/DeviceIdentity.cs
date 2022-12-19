using IOLinkNET.IODD.Structure.DataTypes;
using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.Profile;

public record DeviceIdentityT(ushort VendorId, uint DeviceId, TextRefT VendorName, TextRefT VendorUrl, TextRefT DeviceName, TextRefT DeviceFamily, string? VendorLogo);