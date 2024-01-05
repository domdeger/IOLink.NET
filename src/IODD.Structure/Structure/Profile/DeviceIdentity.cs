using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Interfaces.Profile;

namespace IOLinkNET.IODD.Structure.Profile;

public record DeviceIdentityT(ushort VendorId, uint DeviceId, string VendorName, TextRefT VendorText, TextRefT VendorUrl, TextRefT DeviceName, TextRefT DeviceFamily): IDeviceIdentityT;