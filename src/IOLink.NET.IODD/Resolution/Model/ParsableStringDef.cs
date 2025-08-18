using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Resolution;

public record ParsableStringDef(string Name, ushort Length, StringTEncoding Encoding) : ParsableSimpleDatatypeDef(Name, KindOfSimpleType.String, Length);
