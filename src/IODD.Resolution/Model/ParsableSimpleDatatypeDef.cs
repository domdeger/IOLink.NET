namespace IOLinkNET.IODD.Resolution;

public record ParsableSimpleDatatypeDef(string Name, KindOfSimpleType Datatype, ushort Length) : ParsableDatatype(Name);
