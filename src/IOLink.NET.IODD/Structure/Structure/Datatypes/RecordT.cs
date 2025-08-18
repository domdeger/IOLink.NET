namespace IOLink.NET.IODD.Structure.Datatypes;

public record RecordT(string? Id, ushort BitLength, IEnumerable<RecordItemT> Items, bool SubindexAccessSupported = true)
                : ComplexDatatypeT(Id, SubindexAccessSupported);
