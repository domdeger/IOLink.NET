using IOLink.NET.Conversion;
using IOLink.NET.Core.Contracts;
using IOLink.NET.Core.Models;

namespace IOLink.NET.Integration;

/// <summary>
/// Responsible for reading process data from an IO-Link device and converting it to typed results.
/// </summary>
public class ProcessDataReader
{
    private readonly IMasterConnection _connection;
    private readonly IIoddDataConverter _converter;
    private readonly ConversionResultWrapper _resultWrapper;

    public ProcessDataReader(IMasterConnection connection, IIoddDataConverter converter)
    {
        _connection = connection;
        _converter = converter;
        _resultWrapper = new ConversionResultWrapper();
    }

    /// <summary>
    /// Reads and converts process data input, returning a typed result.
    /// </summary>
    /// <param name="context">The port context.</param>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    /// <exception cref="InvalidOperationException">Thrown when device has no process data in declared.</exception>
    public async Task<ConversionResult> ReadProcessDataInAsync(PortContext context)
    {
        if (context.PdIn is null)
        {
            throw new InvalidOperationException("Device has no process data in declared.");
        }

        var value = await _connection.ReadProcessDataInAsync(context.Port);
        var convertedValue = _converter.Convert(context.PdIn, value.Span);
        return _resultWrapper.WrapConversionResult(convertedValue);
    }

    /// <summary>
    /// Reads and converts process data output, returning a typed result.
    /// </summary>
    /// <param name="context">The port context.</param>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    /// <exception cref="InvalidOperationException">Thrown when device has no process data out declared.</exception>
    public async Task<ConversionResult> ReadProcessDataOutAsync(PortContext context)
    {
        if (context.PdOut is null)
        {
            throw new InvalidOperationException("Device has no process data out declared.");
        }

        var value = await _connection.ReadProcessDataOutAsync(context.Port);
        var convertedValue = _converter.Convert(context.PdOut, value.Span);
        return _resultWrapper.WrapConversionResult(convertedValue);
    }

    /// <summary>
    /// Reads and converts process data input, returning the raw object (obsolete method).
    /// </summary>
    /// <param name="context">The port context.</param>
    /// <returns>The raw converted object.</returns>
    [Obsolete("Use ReadProcessDataInAsync instead for better type safety.")]
    public async Task<object> ReadProcessDataInRawAsync(PortContext context)
    {
        if (context.PdIn is null)
        {
            throw new InvalidOperationException("Device has no process data in declared.");
        }

        var value = await _connection.ReadProcessDataInAsync(context.Port);
        var convertedValue = _converter.Convert(context.PdIn, value.Span);
        return convertedValue;
    }

    /// <summary>
    /// Reads and converts process data output, returning the raw object (obsolete method).
    /// </summary>
    /// <param name="context">The port context.</param>
    /// <returns>The raw converted object.</returns>
    [Obsolete("Use ReadProcessDataOutAsync instead for better type safety.")]
    public async Task<object> ReadProcessDataOutRawAsync(PortContext context)
    {
        if (context.PdOut is null)
        {
            throw new InvalidOperationException("Device has no process data out declared.");
        }

        var value = await _connection.ReadProcessDataOutAsync(context.Port);
        var convertedValue = _converter.Convert(context.PdOut, value.Span);
        return convertedValue;
    }
}
