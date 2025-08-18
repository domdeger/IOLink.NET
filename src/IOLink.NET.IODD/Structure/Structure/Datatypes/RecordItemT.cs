using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Interfaces;

namespace IOLink.NET.IODD.Structure.Datatypes;

public record RecordItemT(byte Subindex, ushort BitOffset, TextRefT Name, TextRefT? Description, SimpleDatatypeT? Type, DatatypeRefT? Ref)
        : IDatatypeOrTypeRef
{
    DatatypeT? IDatatypeOrTypeRef.Type => Type;
}
