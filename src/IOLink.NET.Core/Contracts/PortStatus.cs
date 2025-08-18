namespace IOLink.NET.Core.Contracts;

[Flags]
public enum PortStatus : byte
{
    Disconnected = 0,
    Connected = 1,
    IOLink = 2,
    Error = 4,
    DI = 8,
}
