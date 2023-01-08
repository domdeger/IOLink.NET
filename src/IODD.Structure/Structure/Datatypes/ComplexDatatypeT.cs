namespace IOLinkNET.IODD.Structure.Datatypes;

public abstract record ComplexDatatypeT(string? Id, bool SubindexAccessSupported = true) : DatatypeT(Id);