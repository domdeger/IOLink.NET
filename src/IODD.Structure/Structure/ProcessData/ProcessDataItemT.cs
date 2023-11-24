using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Interfaces;

namespace IOLinkNET.IODD.Structure.ProcessData;

public record ProcessDataItemT(DatatypeT? Datatype, DatatypeRefT? Ref, string Id, ushort BitLength) : IDatatypeOrTypeRef
{
    public DatatypeT? Type => Datatype;
}
