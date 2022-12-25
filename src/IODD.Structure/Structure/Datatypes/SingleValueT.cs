using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.Datatypes;

public record SingleValueT<T>(T LowerValue, T UpperValue, TextRefT? Name) : AbstractValueT(Name) where T : struct;