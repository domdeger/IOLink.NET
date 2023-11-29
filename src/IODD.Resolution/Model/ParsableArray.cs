namespace IOLinkNET.IODD.Resolution;

public record ParsableArray(string Name, ParsableSimpleDatatypeDef Type, ushort Length) : ParsableComplexDataTypeDef(Name);
