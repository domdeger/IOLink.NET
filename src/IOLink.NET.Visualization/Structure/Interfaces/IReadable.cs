using IOLink.NET.Core.Contracts;

namespace IOLink.NET.Visualization.Structure.Interfaces;

internal interface IReadable
{
    IIODDPortReader IoddPortReader { get; }
    Task ReadAsync();
}
