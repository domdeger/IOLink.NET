namespace IOLinkNET.IODD.Resolution;

/// <summary>
/// Represents a parsable data type definition.
/// </summary>
/// <param name="Name">Name of the data type definition.</param>
/// <param name="Datatype">The underlying simple type</param>
/// <param name="Length">Length of simple data type in bits.</param>
public record ParsableSimpleDatatypeDef(string Name, KindOfSimpleType Datatype, ushort Length) : ParsableDatatype(Name);
