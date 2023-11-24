using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.ProcessData;
using IOLinkNET.IODD.Structure.Profile;

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