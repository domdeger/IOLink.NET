using IOLinkNET.Conversion;
using IOLinkNET.Device.Contract;
using IOLinkNET.IODD.Provider;
using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Resolution.Contracts;
using IOLinkNET.IODD.Structure;

namespace IOLinkNET.Integration;

public class IODDPortReader
{
    private readonly IMasterConnection _connection;
    private readonly IDeviceDefinitionProvider _deviceDefinitionProvider;
    private readonly IIoddDataConverter _ioddDataConverter;
    private IProcessDataTypeResolver? _processDataTypeResolver;
    private IParameterTypeResolver? _parameterTypeResolver;
    private ParsableDatatype? _pdInType;
    private ParsableDatatype? _pdOutType;
    private IODevice? _deviceDefinition;

    private byte? _port;

    public IODDPortReader(IMasterConnection connection, IDeviceDefinitionProvider deviceDefinitionProvider, IIoddDataConverter ioddDataConverter)
    {
        _connection = connection;
        _deviceDefinitionProvider = deviceDefinitionProvider;
        _ioddDataConverter = ioddDataConverter;
    }

    public async Task InitializeForPortAsync(byte port)
    {
        var portInfo = await _connection.GetPortInformationAsync(port);
        if (portInfo.Status != PortStatus.IOLink)
        {
            throw new InvalidOperationException("Port is not in IO-Link mode");
        }

        if (portInfo.DeviceInformation is null)
        {
            throw new InvalidOperationException($"Device information is not available for requested port {port}");
        }

        _deviceDefinition = await _deviceDefinitionProvider.GetDeviceDefinitionAsync(portInfo.DeviceInformation.VendorId, portInfo.DeviceInformation.DeviceId, portInfo.DeviceInformation.ProductId);
        _processDataTypeResolver = new ProcessDataTypeResolver(_deviceDefinition);
        _parameterTypeResolver = new ParameterTypeResolver(_deviceDefinition);

        (_pdInType, _pdOutType) = await GetProcessDataTypesAsync(port, _processDataTypeResolver);
        _port = port;
    }

    public async Task<object> ReadParameter(ushort index, byte subindex)
    {
        var value = await _connection.ReadIndexAsync(_port!.Value, index, subindex);
        var paramTypeDef = _parameterTypeResolver!.GetParameter(index, subindex);

        var convertedValue = _ioddDataConverter.Convert(paramTypeDef, value.Span);

        return convertedValue;
    }

    private async Task<(ParsableDatatype? PdIn, ParsableDatatype? PdOut)> GetProcessDataTypesAsync(byte port, IProcessDataTypeResolver processDataTypeResolver)
    {
        ParsableDatatype? pdInType;
        ParsableDatatype? pdOutType;
        if (processDataTypeResolver.HasCondition())
        {
            var condition = processDataTypeResolver.ResolveCondition();
            var conditionValue = await _connection.ReadIndexAsync(port, condition.VariableDef.Index, condition.ConditionDef.Subindex);
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
