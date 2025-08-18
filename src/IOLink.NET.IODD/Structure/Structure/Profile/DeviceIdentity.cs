using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Interfaces.Profile;

namespace IOLink.NET.IODD.Structure.Profile;

public record DeviceIdentityT(ushort VendorId, uint DeviceId, string VendorName, TextRefT VendorText, TextRefT VendorUrl, TextRefT DeviceName, TextRefT DeviceFamily): IDeviceIdentityT;
