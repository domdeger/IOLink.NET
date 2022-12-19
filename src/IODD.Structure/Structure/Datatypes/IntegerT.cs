namespace IOLinkNET.IODD.Structure.DataTypes;

public record IntegerT(string? Id, ushort BitLength, IEnumerable<SingleValueT<int>> SingleValues, IEnumerable<ValueRangeT<int>> ValueRanges) : NumberT(Id);