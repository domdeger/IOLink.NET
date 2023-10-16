namespace IOLinkNET.Device.Contract;

public interface IPortInformation
{
    PortStatus Status { get; }

    byte PortNumber { get; }

    IDeviceInformation? DeviceInformation { get; }
}