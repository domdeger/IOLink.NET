using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Structure.Interfaces;

public interface IDatatypeOrTypeRef
{
    DatatypeT? Type { get; }
    DatatypeRefT? Ref { get; }
}
