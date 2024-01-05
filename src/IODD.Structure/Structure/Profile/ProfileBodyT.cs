using IOLinkNET.IODD.Structure.Interfaces.Profile;

namespace IOLinkNET.IODD.Structure.Profile;

public record ProfileBodyT(IDeviceIdentityT DeviceIdentity, IDeviceFunctionT DeviceFunction): IProfileBodyT;