using IOLinkNET.IODD.Structure.DataTypes;

namespace IODD.Structure.Structure.Datatypes;

public record RecordT(string? Id, ushort BitLength, IEnumerable<RecordItemT> Items, bool SubindexAccessSupported = true)
                : ComplexDatatypeT(Id, SubindexAccessSupported);