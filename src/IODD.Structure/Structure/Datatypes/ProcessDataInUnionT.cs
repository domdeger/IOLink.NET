using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Structure.Structure.Datatypes;
public record ProcessDataInUnionT(string Id, SimpleDatatypeT? Type, DatatypeRefT? Ref) : ProcessDataUnionT(Id, Type, Ref);
