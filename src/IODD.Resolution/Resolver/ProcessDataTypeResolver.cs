using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Resolution;

public class ProcessDataTypeResolver
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
    {
        var condition = _device.ProfileBody.DeviceFunction.ProcessDataCollection.FirstOrDefault(pd => pd.Condition is not null)?.Condition
            ?? throw new InvalidOperationException("No process data condition available. ");

        var variable = _device.ProfileBody.DeviceFunction.VariableCollection.First(v => v.Id == condition.VariableId);

        return new(condition, variable);
    }

    public ParsableDatatype ResolveProcessDataIn(int? condition = null)
    {
        var pd = _device.ProfileBody.DeviceFunction.ProcessDataCollection.FirstOrDefault(pd => pd.Condition?.Value == condition)
            ?? throw new ArgumentOutOfRangeException(nameof(condition));

        return ResolveProcessData(pd.ProcessDataIn ?? throw new InvalidOperationException("ProcessDataIn is null."));
    }

    public ParsableDatatype ResolveProcessDataOut(int? condition = null)
    {
        var pd = _device.ProfileBody.DeviceFunction.ProcessDataCollection.FirstOrDefault(pd => pd.Condition?.Value == condition)
            ?? throw new ArgumentOutOfRangeException(nameof(condition));

        return ResolveProcessData(pd.ProcessDataOut ?? throw new InvalidOperationException("ProcessDataOut is null."));
    }

    private ParsableDatatype ResolveProcessData(ProcessDataItemT processData)
        => _converter.Convert(processData);
}