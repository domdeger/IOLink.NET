using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Interfaces;

namespace IOLinkNET.IODD.Structure.Datatypes;

public record ArrayT(string? Id, byte Count, SimpleDatatypeT? Type, DatatypeRefT? Ref, bool SubindexAccessSupported = true)
    : ComplexDatatypeT(Id, SubindexAccessSupported), IDatatypeOrTypeRef
{
    DatatypeT? IDatatypeOrTypeRef.Type => Type;
}
