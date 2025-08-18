using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Interfaces;

namespace IOLink.NET.IODD.Structure.ProcessData;

public record ProcessDataItemT(DatatypeT? Datatype, DatatypeRefT? Ref, string Id, ushort BitLength) : IDatatypeOrTypeRef
{
    public DatatypeT? Type => Datatype;
}
