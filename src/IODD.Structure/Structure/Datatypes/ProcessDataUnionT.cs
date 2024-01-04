using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Interfaces;

namespace IOLinkNET.IODD.Structure.Structure.Datatypes;
public record ProcessDataUnionT(string? Id, SimpleDatatypeT? Type, DatatypeRefT? Ref) : DatatypeT(Id), IDatatypeOrTypeRef
{
    DatatypeT? IDatatypeOrTypeRef.Type => Type;
}
