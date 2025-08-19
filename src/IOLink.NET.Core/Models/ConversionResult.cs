namespace IOLink.NET.Core.Models;

/// <summary>
/// Base class for conversion results from IODD data conversion operations.
/// </summary>
public abstract record ConversionResult;

/// <summary>
/// Represents a scalar conversion result containing a primitive or simple value.
/// </summary>
/// <param name="Value">The converted scalar value.</param>
/// <param name="ClrType">The CLR type used to represent the scalar value.</param>
public sealed record ScalarResult(object Value, Type ClrType) : ConversionResult;

/// <summary>
/// Represents a complex conversion result containing a list of key-value mappings.
/// </summary>
/// <param name="Values">A list of key-value pairs where each value is a ScalarResult.</param>
public sealed record ComplexResult(IReadOnlyList<KeyValuePair<string, ScalarResult>> Values)
    : ConversionResult;
