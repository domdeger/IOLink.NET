namespace IOLinkNET.IODD.Structure.Datatypes;

public record OctetStringT(string? Id, byte FixedLength) : SimpleDatatypeT(Id);