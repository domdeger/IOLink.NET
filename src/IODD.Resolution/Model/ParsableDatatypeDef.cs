namespace IODD.Resolution.Model;

public record ParsableDatatype(string Name);
public record ParsableSimpleDatatypeDef(string Name, KindOfSimpleType Datatype, ushort? Length) : ParsableDatatype(Name);
public record ParsableRecord(string Name, IEnumerable<ParsableRecordItem> Entries) : ParsableDatatype(Name);
public record ParsableRecordItem(ParsableSimpleDatatypeDef Type, string Name, ushort BitOffset, ushort Subindex);

public enum KindOfSimpleType
{
    Integer,
    UInteger,
    Boolean,
    Float,
    String,
    OctetString,
    TimespanT
}