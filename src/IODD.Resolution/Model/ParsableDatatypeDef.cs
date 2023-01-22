using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Resolution;

public record ParsableDatatype(string Name);
public record ParsableSimpleDatatypeDef(string Name, KindOfSimpleType Datatype, ushort Length) : ParsableDatatype(Name);
public record ParsableStringDef(string Name, ushort Length, StringTEncoding Encoding) : ParsableSimpleDatatypeDef(Name, KindOfSimpleType.String, Length);

public record ParsableComplexDataTypeDef(string Name) : ParsableDatatype(Name);
public record ParsableRecord(string Name, IEnumerable<ParsableRecordItem> Entries) : ParsableComplexDataTypeDef(Name);
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