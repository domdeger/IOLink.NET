[Flags]
public enum PortStatus : byte
{
    Connected = 1,
    Disconnected = 2,
    Error = 8,
    IOLink = 8,
    DI = 16
}