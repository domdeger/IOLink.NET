namespace IOLinkNET.IODD.Structure.DataTypes;

public record RecordT(string? Id, ushort BitLength, IEnumerable<RecordItemT> Items, bool SubindexAccessSupported = true)
                : ComplexDatatypeT(Id, SubindexAccessSupported);