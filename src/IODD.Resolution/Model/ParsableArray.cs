namespace IOLinkNET.IODD.Resolution;

public record ParsableArray(string Name, ParsableSimpleDatatypeDef Type, bool SubindexAccessSupported, ushort Length)
    : ParsableComplexDataTypeDef(Name, SubindexAccessSupported);
