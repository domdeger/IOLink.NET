using IOLink.NET.Core.Models;

namespace IOLink.NET.Core.Contracts;

/// <summary>
/// Interface for reading and converting IODD data with type-safe result wrappers.
/// Provides methods that return ConversionResult objects distinguishing between scalar and complex values.
/// </summary>
public interface IIODDPortReader
{
    /// <summary>
    /// Reads and converts a parameter value, returning a typed result that distinguishes between scalar and complex values.
    /// </summary>
    /// <param name="index">The parameter index.</param>
    /// <param name="subindex">The parameter subindex.</param>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    Task<ConversionResult> ReadConvertedParameterResultAsync(ushort index, byte subindex);

    /// <summary>
    /// Reads and converts process data input, returning a typed result that distinguishes between scalar and complex values.
    /// </summary>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    Task<ConversionResult> ReadConvertedProcessDataInResultAsync();

    /// <summary>
    /// Reads and converts process data output, returning a typed result that distinguishes between scalar and complex values.
    /// </summary>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    Task<ConversionResult> ReadConvertedProcessDataOutResultAsync();
}
