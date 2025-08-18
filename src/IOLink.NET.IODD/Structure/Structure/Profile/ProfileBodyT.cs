using IOLink.NET.IODD.Structure.Interfaces.Profile;

namespace IOLink.NET.IODD.Structure.Profile;

public record ProfileBodyT(IDeviceIdentityT DeviceIdentity, IDeviceFunctionT DeviceFunction): IProfileBodyT;
