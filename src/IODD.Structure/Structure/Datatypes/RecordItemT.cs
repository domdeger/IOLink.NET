using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.DataTypes;

public record RecordItemT(string DatatypeId, byte Subindex, ushort BitOffset, TextRefT Name,
                             TextRefT? Description, SimpleDatatypeT? Type, DatatypeRefT? Ref, AccessRightsT? AccessRightRestriction);