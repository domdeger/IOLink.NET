namespace IOLinkNET.IODD.Structure.Datatypes;

public record UIntegerT(string? Id, ushort BitLength, IEnumerable<SingleValueT<uint>> SingleValues, IEnumerable<ValueRangeT<uint>> ValueRanges) : NumberT(Id);