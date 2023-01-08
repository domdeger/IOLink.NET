using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Interfaces;

namespace IOLinkNET.IODD.Structure.Datatypes;

public record RecordItemT(byte Subindex, ushort BitOffset, TextRefT Name, TextRefT? Description, SimpleDatatypeT? Type, DatatypeRefT? Ref)
        : IDatatypeOrTypeRef
{
    DatatypeT? IDatatypeOrTypeRef.Type => Type;
}
