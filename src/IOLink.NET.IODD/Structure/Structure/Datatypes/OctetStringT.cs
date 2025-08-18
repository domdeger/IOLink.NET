namespace IOLink.NET.IODD.Structure.Datatypes;

public record OctetStringT(string? Id, byte FixedLength) : SimpleDatatypeT(Id);
