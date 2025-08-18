using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Interfaces;

namespace IOLink.NET.IODD.Structure.Structure.Datatypes;
public record ProcessDataUnionT(string? Id, SimpleDatatypeT? Type, DatatypeRefT? Ref) : DatatypeT(Id), IDatatypeOrTypeRef
{
    DatatypeT? IDatatypeOrTypeRef.Type => Type;
}
