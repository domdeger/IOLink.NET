using IOLink.NET.Conversion;
using IOLink.NET.Core.Contracts;
using IOLink.NET.IODD.Provider;
using IOLink.NET.IODD.Resolution;
using IOLink.NET.IODD.Resolution.Contracts;
using IOLink.NET.IODD.Structure;

namespace IOLink.NET.Integration;

public class IODDPortReader(
    IMasterConnection connection,
    IDeviceDefinitionProvider deviceDefinitionProvider,
    IIoddDataConverter ioddDataConverter,
    ITypeResolverFactory typeResolverFactory
)
{
    private readonly IMasterConnection _connection = connection;
    private readonly IDeviceDefinitionProvider _deviceDefinitionProvider = deviceDefinitionProvider;
    private readonly IIoddDataConverter _ioddDataConverter = ioddDataConverter;
    private readonly ITypeResolverFactory _typeResolverFactory = typeResolverFactory;
    private PortReaderInitilizationResult? _initilizationState;
    private PortReaderInitilizationResult InitilizationState =>
        _initilizationState ?? throw new InvalidOperationException("PortReader is not initialized");

    public IODevice Device
    {
        get
        {
            _ =
                _initilizationState
                ?? throw new InvalidOperationException("PortReader is not initialized");
            return InitilizationState.DeviceDefinition;
        }
    }

    public async Task InitializeForPortAsync(byte port)
    {
        var portInfo = await _connection.GetPortInformationAsync(port);
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

        var deviceDefinition = await _deviceDefinitionProvider.GetDeviceDefinitionAsync(
            portInfo.DeviceInformation.VendorId,
            portInfo.DeviceInformation.DeviceId,
            portInfo.DeviceInformation.ProductId
        );
        var pdDataResolver = _typeResolverFactory.CreateProcessDataTypeResolver(deviceDefinition);
        var paramDataResolver = _typeResolverFactory.CreateParameterTypeResolver(deviceDefinition);

        var (pdInType, pdOutType) = await GetProcessDataTypesAsync(port, pdDataResolver);
        _initilizationState = new PortReaderInitilizationResult(
            pdInType,
            pdOutType,
            port,
            pdDataResolver,
            paramDataResolver,
            deviceDefinition
        );
    }

    public virtual async Task<object> ReadConvertedParameterAsync(ushort index, byte subindex)
    {
        var paramTypeDef = InitilizationState.ParameterTypeResolver.GetParameter(index, subindex);

        var value = await _connection.ReadIndexAsync(
            InitilizationState.Port,
            index,
            paramTypeDef.SubindexAccessSupported ? subindex : (byte)0
        );

        var convertedValue = _ioddDataConverter.Convert(paramTypeDef, value.Span);

        return convertedValue;
    }

    public async Task<object> ReadConvertedProcessDataInAsync()
    {
        if (InitilizationState.PdIn is null)
        {
            throw new InvalidOperationException("Device has no process data in declared.");
        }

        var value = await _connection.ReadProcessDataInAsync(InitilizationState.Port);
        var convertedValue = _ioddDataConverter.Convert(InitilizationState.PdIn, value.Span);

        return convertedValue;
    }

    public async Task<object> ReadConvertedProcessDataOutAsync()
    {
        if (InitilizationState.PdOut is null)
        {
            throw new InvalidOperationException("Device has no process data out declared.");
        }

        var value = await _connection.ReadProcessDataOutAsync(InitilizationState.Port);
        var convertedValue = _ioddDataConverter.Convert(InitilizationState.PdOut, value.Span);

        return convertedValue;
    }

    private async Task<(ParsableDatatype? PdIn, ParsableDatatype? PdOut)> GetProcessDataTypesAsync(
        byte port,
        IProcessDataTypeResolver processDataTypeResolver
    )
    {
        ParsableDatatype? pdInType;
        ParsableDatatype? pdOutType;
        if (processDataTypeResolver.HasCondition())
        {
            var condition = processDataTypeResolver.ResolveCondition();
            var conditionValue = await _connection.ReadIndexAsync(
                port,
                condition.VariableDef.Index,
                condition.ConditionDef.Subindex ?? 0
            );
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

    public record PortReaderInitilizationResult(
        ParsableDatatype? PdIn,
        ParsableDatatype? PdOut,
        byte Port,
        IProcessDataTypeResolver ProcessDataTypeResolver,
        IParameterTypeResolver ParameterTypeResolver,
        IODevice DeviceDefinition
    );
}
