using IOLink.NET.Conversion;
using IOLink.NET.Core.Contracts;
using IOLink.NET.Core.Models;
using IOLink.NET.IODD.Provider;
using IOLink.NET.IODD.Resolution;
using IOLink.NET.IODD.Resolution.Contracts;
using IOLink.NET.IODD.Structure;

namespace IOLink.NET.Integration;

/// <summary>
/// Main orchestrator for reading IODD data from IO-Link devices.
/// This class coordinates between port initialization, parameter reading, and process data reading.
/// </summary>
public class IODDPortReader : IIODDPortReader
{
    private readonly PortInitializer _portInitializer;
    private readonly ParameterDataReader _parameterDataReader;
    private readonly ProcessDataReader _processDataReader;

    private PortContext? _portContext;

    public IODDPortReader(
        IMasterConnection connection,
        IDeviceDefinitionProvider deviceDefinitionProvider,
        IIoddDataConverter ioddDataConverter,
        ITypeResolverFactory typeResolverFactory
    )
    {
        _portInitializer = new PortInitializer(
            connection,
            deviceDefinitionProvider,
            typeResolverFactory
        );
        _parameterDataReader = new ParameterDataReader(connection, ioddDataConverter);
        _processDataReader = new ProcessDataReader(connection, ioddDataConverter);
    }

    private PortContext PortContext =>
        _portContext ?? throw new InvalidOperationException("PortReader is not initialized");

    public IODevice Device
    {
        get
        {
            _ =
                _portContext
                ?? throw new InvalidOperationException("PortReader is not initialized");
            return PortContext.DeviceDefinition;
        }
    }

    public async Task InitializeForPortAsync(byte port, CancellationToken cancellationToken)
    {
        _portContext = await _portInitializer
            .InitializePortAsync(port, cancellationToken)
            .ConfigureAwait(false);
    }

    [Obsolete("Use ReadConvertedParameterResultAsync instead for better type safety.")]
    public virtual Task<object> ReadConvertedParameterAsync(
        ushort index,
        byte subindex,
        CancellationToken cancellationToken
    )
    {
        return _parameterDataReader.ReadParameterRawAsync(
            PortContext,
            index,
            subindex,
            cancellationToken
        );
    }

    /// <summary>
    /// Reads and converts a parameter value, returning a typed result that distinguishes between scalar and complex values.
    /// </summary>
    /// <param name="index">The parameter index.</param>
    /// <param name="subindex">The parameter subindex.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    public virtual Task<ConversionResult> ReadConvertedParameterResultAsync(
        ushort index,
        byte subindex,
        CancellationToken cancellationToken
    )
    {
        return _parameterDataReader.ReadParameterAsync(
            PortContext,
            index,
            subindex,
            cancellationToken
        );
    }

    [Obsolete("Use ReadConvertedProcessDataInResultAsync instead for better type safety.")]
    public Task<object> ReadConvertedProcessDataInAsync(CancellationToken cancellationToken)
    {
        return _processDataReader.ReadProcessDataInRawAsync(PortContext, cancellationToken);
    }

    /// <summary>
    /// Reads and converts process data input, returning a typed result that distinguishes between scalar and complex values.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    public Task<ConversionResult> ReadConvertedProcessDataInResultAsync(
        CancellationToken cancellationToken
    )
    {
        return _processDataReader.ReadProcessDataInAsync(PortContext, cancellationToken);
    }

    [Obsolete("Use ReadConvertedProcessDataOutResultAsync instead for better type safety.")]
    public Task<object> ReadConvertedProcessDataOutAsync(CancellationToken cancellationToken)
    {
        return _processDataReader.ReadProcessDataOutRawAsync(PortContext, cancellationToken);
    }

    /// <summary>
    /// Reads and converts process data output, returning a typed result that distinguishes between scalar and complex values.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A ConversionResult containing either a ScalarResult or ComplexResult.</returns>
    public Task<ConversionResult> ReadConvertedProcessDataOutResultAsync(
        CancellationToken cancellationToken
    )
    {
        return _processDataReader.ReadProcessDataOutAsync(PortContext, cancellationToken);
    }
}
