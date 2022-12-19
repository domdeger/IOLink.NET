namespace IOLinkNET.IODD.Structure.DataTypes;

public abstract record ComplexDatatypeT(string? Id, bool SubindexAccessSupported = true) : DatatypeT(Id);