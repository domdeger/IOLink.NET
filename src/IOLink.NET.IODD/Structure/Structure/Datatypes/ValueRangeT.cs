using IOLink.NET.IODD.Structure.Common;

namespace IOLink.NET.IODD.Structure.Datatypes;

public record ValueRangeT<T>(T LowerValue, T UpperValue, TextRefT? Name) : AbstractValueT(Name) where T : struct;
