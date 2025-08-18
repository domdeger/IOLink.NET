using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Structure.Structure.Datatypes;
public record ProcessDataOutUnionT(string Id, SimpleDatatypeT? Type, DatatypeRefT? Ref) : ProcessDataUnionT(Id, Type, Ref);
