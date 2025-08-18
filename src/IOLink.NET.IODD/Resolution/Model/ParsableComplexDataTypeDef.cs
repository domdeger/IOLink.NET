namespace IOLink.NET.IODD.Resolution;

public record ParsableComplexDataTypeDef(string Name, bool SubindexAccessSupported) : ParsableDatatype(Name, SubindexAccessSupported);
