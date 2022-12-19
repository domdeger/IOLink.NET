using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.Profile;

public record DeviceFunctionT(ushort VendorId, uint DeviceId, TextRefT VendorName, TextRefT VendorUrl, TextRefT DeviceName, TextRefT DeviceFamily, string? VendorLogo);