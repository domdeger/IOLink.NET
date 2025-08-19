using IOLink.NET.Core.Contracts;
using IOLink.NET.IODD.Provider;
using IOLink.NET.IODD.Resolution;
using IOLink.NET.IODD.Resolution.Contracts;

namespace IOLink.NET.Integration;

/// <summary>
/// Responsible for initializing a port by resolving device definitions and type resolvers.
/// </summary>
public class PortInitializer
{
    private readonly IMasterConnection _connection;
    private readonly IDeviceDefinitionProvider _deviceDefinitionProvider;
    private readonly ITypeResolverFactory _typeResolverFactory;

    public PortInitializer(
        IMasterConnection connection,
        IDeviceDefinitionProvider deviceDefinitionProvider,
        ITypeResolverFactory typeResolverFactory
    )
    {
        _connection = connection;
        _deviceDefinitionProvider = deviceDefinitionProvider;
        _typeResolverFactory = typeResolverFactory;
    }

    /// <summary>
    /// Initializes the port by getting device information and creating type resolvers.
    /// </summary>
    /// <param name="port">The port number to initialize.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A PortContext containing all resolved information.</returns>
    /// <exception cref="InvalidOperationException">Thrown when port is not in IO-Link mode or device information is not available.</exception>
    public async Task<PortContext> InitializePortAsync(
        byte port,
        CancellationToken cancellationToken
    )
    {
        var portInfo = await _connection
            .GetPortInformationAsync(port, cancellationToken)
            .ConfigureAwait(false);
        if (!portInfo.Status.HasFlag(PortStatus.IOLink))
        {
            throw new InvalidOperationException("Port is not in IO-Link mode");
        }

        if (portInfo.DeviceInformation is null)
        {
            throw new InvalidOperationException(
                $"Device information is not available for requested port {port}"
            );
        }

        var deviceDefinition = await _deviceDefinitionProvider
            .GetDeviceDefinitionAsync(
                portInfo.DeviceInformation.VendorId,
                portInfo.DeviceInformation.DeviceId,
                portInfo.DeviceInformation.ProductId,
                cancellationToken
            )
            .ConfigureAwait(false);

        var pdDataResolver = _typeResolverFactory.CreateProcessDataTypeResolver(deviceDefinition);
        var paramDataResolver = _typeResolverFactory.CreateParameterTypeResolver(deviceDefinition);

        var (pdInType, pdOutType) = await ResolveProcessDataTypesAsync(
                port,
                pdDataResolver,
                cancellationToken
            )
            .ConfigureAwait(false);

        return new PortContext(
            port,
            deviceDefinition,
            pdDataResolver,
            paramDataResolver,
            pdInType,
            pdOutType
        );
    }

    private async Task<(
        ParsableDatatype? PdIn,
        ParsableDatatype? PdOut
    )> ResolveProcessDataTypesAsync(
        byte port,
        IProcessDataTypeResolver processDataTypeResolver,
        CancellationToken cancellationToken
    )
    {
        ParsableDatatype? pdInType;
        ParsableDatatype? pdOutType;

        if (processDataTypeResolver.HasCondition())
        {
            var condition = processDataTypeResolver.ResolveCondition();
            var conditionValue = await _connection
                .ReadIndexAsync(
                    port,
                    condition.VariableDef.Index,
                    cancellationToken,
                    condition.ConditionDef.Subindex ?? 0
                )
                .ConfigureAwait(false);
            pdInType = processDataTypeResolver.ResolveProcessDataIn(conditionValue.Span[0]);
            pdOutType = processDataTypeResolver.ResolveProcessDataOut(conditionValue.Span[0]);
        }
        else
        {
            pdInType = processDataTypeResolver.ResolveProcessDataIn();
            pdOutType = processDataTypeResolver.ResolveProcessDataOut();
        }

        return (pdInType, pdOutType);
    }
}
