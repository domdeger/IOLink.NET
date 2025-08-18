using IOLink.NET.Integration;

namespace IOLink.NET.Visualization.Structure.Interfaces;
internal interface IReadable
{
    IODDPortReader IoddPortReader { get; }
    Task ReadAsync();
}
