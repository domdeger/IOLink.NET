namespace IOLinkNET.IODD.Structure.DataTypes;

public record BooleanT(string? Id, IEnumerable<SingleValueT<bool>> SingleValues) : SimpleDatatypeT(Id);