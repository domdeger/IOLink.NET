namespace IOLink.NET.IODD.Resolution;

public record ParsableRecord(string Name, ushort Length, bool SubindexAccessSupported, IEnumerable<ParsableRecordItem> Entries)
    : ParsableComplexDataTypeDef(Name, SubindexAccessSupported);
