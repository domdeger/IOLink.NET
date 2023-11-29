namespace IOLinkNET.IODD.Resolution;

public record ParsableRecord(string Name, ushort Length, IEnumerable<ParsableRecordItem> Entries) : ParsableComplexDataTypeDef(Name);
