namespace IOLinkNET.IODD.Structure.DataTypes;

public record Float32T(string? Id, IEnumerable<SingleValueT<float>> SingleValues, IEnumerable<ValueRangeT<float>> ValueRanges) : NumberT(Id);