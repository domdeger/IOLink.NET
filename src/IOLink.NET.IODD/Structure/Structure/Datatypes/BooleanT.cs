namespace IOLink.NET.IODD.Structure.Datatypes;

public record BooleanT(string? Id, IEnumerable<SingleValueT<bool>> SingleValues) : SimpleDatatypeT(Id);
