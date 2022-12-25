using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.DataTypes;

namespace IODD.Structure.Structure.Datatypes;

public record ArrayT(string? Id, byte Count, SimpleDatatypeT? Type, DatatypeRefT? Ref, bool SubindexAccessSupported = true) : ComplexDatatypeT(Id, SubindexAccessSupported);
