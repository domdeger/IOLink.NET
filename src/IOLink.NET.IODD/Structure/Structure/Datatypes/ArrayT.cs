using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Interfaces;

namespace IOLink.NET.IODD.Structure.Datatypes;

public record ArrayT(string? Id, byte Count, SimpleDatatypeT? Type, DatatypeRefT? Ref, bool SubindexAccessSupported = true)
    : ComplexDatatypeT(Id, SubindexAccessSupported), IDatatypeOrTypeRef
{
    DatatypeT? IDatatypeOrTypeRef.Type => Type;
}
