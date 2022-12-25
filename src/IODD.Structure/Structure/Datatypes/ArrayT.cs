using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.Datatypes;

public record ArrayT(string? Id, byte Count, SimpleDatatypeT? Type, DatatypeRefT? Ref, bool SubindexAccessSupported = true) : ComplexDatatypeT(Id, SubindexAccessSupported);
