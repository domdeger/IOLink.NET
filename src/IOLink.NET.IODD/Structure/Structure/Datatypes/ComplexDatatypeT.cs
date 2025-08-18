namespace IOLink.NET.IODD.Structure.Datatypes;

public abstract record ComplexDatatypeT(string? Id, bool SubindexAccessSupported = true) : DatatypeT(Id);
