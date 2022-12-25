using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.DataTypes;

namespace IODD.Structure.Structure.Datatypes;

public record RecordItemT(byte Subindex, ushort BitOffset, TextRefT Name, TextRefT? Description, SimpleDatatypeT? Type, DatatypeRefT? Ref);