namespace IOLinkNET.IODD.Structure.DataTypes;

public record OctetStringT(string? Id, byte FixedLength) : SimpleDatatypeT(Id);