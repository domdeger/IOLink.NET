using IOLinkNET.Integration;

namespace IOLinkNET.Visualization.Structure.Interfaces;
internal interface IReadable
{
    IODDPortReader IoddPortReader { get; }
    Task ReadAsync();
}
