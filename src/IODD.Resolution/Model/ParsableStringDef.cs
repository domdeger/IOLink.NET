using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Resolution;

public record ParsableStringDef(string Name, ushort Length, StringTEncoding Encoding) : ParsableSimpleDatatypeDef(Name, KindOfSimpleType.String, Length);
