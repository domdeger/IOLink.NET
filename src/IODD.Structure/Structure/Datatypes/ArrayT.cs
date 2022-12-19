using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Structure.DataTypes;

public record ArrayT(string? Id, SimpleDatatypeT? Type, DatatypeRefT? Ref, bool SubindexAccessSupported = true) : ComplexDatatypeT(Id, SubindexAccessSupported);
