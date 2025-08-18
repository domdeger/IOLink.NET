namespace IOLink.NET.Core.Contracts;

public interface IPortInformation
{
    PortStatus Status { get; }

    byte PortNumber { get; }

    IDeviceInformation? DeviceInformation { get; }
}
