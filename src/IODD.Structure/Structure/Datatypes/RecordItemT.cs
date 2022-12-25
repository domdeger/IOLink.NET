using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.Datatypes;

public record RecordItemT(byte Subindex, ushort BitOffset, TextRefT Name, TextRefT? Description, SimpleDatatypeT? Type, DatatypeRefT? Ref);