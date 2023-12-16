using IOLinkNET.IODD.Resolution.Contracts;
using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Resolution;

public class ProcessDataTypeResolver : IProcessDataTypeResolver
{
    private readonly IODevice _device;

    private readonly ParsableDatatypeConverter _converter;
    private readonly DatatypeResolver _datatypeResolver;

    public ProcessDataTypeResolver(IODevice device)
    {
        _device = device;
        _datatypeResolver = new(_device.ProfileBody.DeviceFunction.DatatypeCollection);
        _converter = new(_datatypeResolver);
    }

    public bool HasCondition() => _device.ProfileBody.DeviceFunction.ProcessDataCollection.Any(pd => pd.Condition is not null);

    public ResolvedCondition ResolveCondition()
        => ResolveConditionInternal(_device.ProfileBody.DeviceFunction.ProcessDataCollection);

    public ParsableDatatype? ResolveProcessDataIn(int? condition = null)
    {
        var pd = FindProcessDataByCondition(condition);
        return pd?.ProcessDataIn is null ? null : ResolveProcessData(pd.ProcessDataIn);
    }

    public ParsableDatatype? ResolveProcessDataOut(int? condition = null)
    {
        var pd = FindProcessDataByCondition(condition);
        return pd?.ProcessDataOut is null ? null : ResolveProcessData(pd.ProcessDataOut);
    }

    private ProcessDataT? FindProcessDataByCondition(int? condition)
    {
        var pd = _device.ProfileBody.DeviceFunction.ProcessDataCollection.FirstOrDefault(pd => pd.Condition?.Value == condition);
        return pd is null && condition is not null
            ? throw new InvalidOperationException($"No process data with condition {condition} available.")
            : pd;
    }

    private ResolvedCondition ResolveConditionInternal(IEnumerable<ProcessDataT> processDataCollection)
    {
        var condition = processDataCollection.FirstOrDefault(pd => pd.Condition is not null)?.Condition
            ?? throw new InvalidOperationException("No process data condition available. ");

        var variable = _device.ProfileBody.DeviceFunction.VariableCollection.First(v => v.Id == condition.VariableId);

        return new(condition, variable);
    }

    private ParsableDatatype ResolveProcessData(ProcessDataItemT processData)
        => _converter.Convert(processData);
}