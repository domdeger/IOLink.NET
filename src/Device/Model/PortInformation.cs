using IOLinkNET.Device.Contract;

namespace IOLinkNET.Device.Model;

public class PortInformation : IPortInformation
{
    public PortInformation(byte portNumber, PortStatus status, IDeviceInformation? deviceInformation)
    {
        PortNumber = portNumber;
        Status = status;
        DeviceInformation = deviceInformation;
    }

    public PortStatus Status { get; }

    public byte PortNumber { get; }

    public IDeviceInformation? DeviceInformation { get; }
}