namespace IOLinkNET.IODD.Structure.Datatypes;

public record IntegerT(string? Id, ushort BitLength, IEnumerable<SingleValueT<int>> SingleValues, IEnumerable<ValueRangeT<int>> ValueRanges) : NumberT(Id);