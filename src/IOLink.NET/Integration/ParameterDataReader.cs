using IOLink.NET.Conversion;
using IOLink.NET.Core.Contracts;
using IOLink.NET.Core.Models;

namespace IOLink.NET.Integration;

/// <summary>
/// Responsible for reading parameter data from an IO-Link device and converting it to typed results.
/// </summary>
public class ParameterDataReader
{
    private readonly IMasterConnection _connection;
    private readonly IIoddDataConverter _converter;
    private readonly ConversionResultWrapper _resultWrapper;

    public ParameterDataReader(IMasterConnection connection, IIoddDataConverter converter)
    {
        _connection = connection;
        _converter = converter;
        _resultWrapper = new ConversionResultWrapper();
    }

    /// <summary>
    /// Reads and converts a parameter value, returning a typed result.
    /// </summary>
    /// <param name="context">The port context.</param>
    /// <param name="index">The parameter index.</param>
    /// <param name="subindex">The parameter subindex.</param>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    public async Task<ConversionResult> ReadParameterAsync(
        PortContext context,
        ushort index,
        byte subindex,
        CancellationToken cancellationToken
    )
    {
        var paramTypeDef = context.ParameterTypeResolver.GetParameter(index, subindex);

        var value = await _connection
            .ReadIndexAsync(
                context.Port,
                index,
                cancellationToken,
                paramTypeDef.SubindexAccessSupported ? subindex : (byte)0
            )
            .ConfigureAwait(false);

        var convertedValue = _converter.Convert(paramTypeDef, value.Span);
        return _resultWrapper.WrapConversionResult(convertedValue);
    }

    /// <summary>
    /// Reads and converts a parameter value, returning the raw object (obsolete method).
    /// </summary>
    /// <param name="context">The port context.</param>
    /// <param name="index">The parameter index.</param>
    /// <param name="subindex">The parameter subindex.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The raw converted object.</returns>
    [Obsolete("Use ReadParameterAsync instead for better type safety.")]
    public async Task<object> ReadParameterRawAsync(
        PortContext context,
        ushort index,
        byte subindex,
        CancellationToken cancellationToken
    )
    {
        var paramTypeDef = context.ParameterTypeResolver.GetParameter(index, subindex);

        var value = await _connection
            .ReadIndexAsync(
                context.Port,
                index,
                cancellationToken,
                paramTypeDef.SubindexAccessSupported ? subindex : (byte)0
            )
            .ConfigureAwait(false);

        var convertedValue = _converter.Convert(paramTypeDef, value.Span);
        return convertedValue;
    }
}
