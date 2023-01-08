using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Structure.Interfaces;

public interface IDatatypeOrTypeRef
{
    DatatypeT? Type { get; }
    DatatypeRefT? Ref { get; }
}