using IOLinkNET.IODD.Resolution.Contracts;
using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Resolution;

public class ParameterTypeResolver : IParameterTypeResolver
{
    private readonly IODevice _device;

    private readonly ParsableDatatypeConverter _converter;
    private readonly DatatypeResolver _datatypeResolver;

    public ParameterTypeResolver(IODevice device)
    {
        _device = device;
        _datatypeResolver = new(_device.ProfileBody.DeviceFunction.DatatypeCollection);
        _converter = new(_datatypeResolver);
    }

    public ParsableDatatype GetParameter(int index, byte? subIndex = null)
    {
        var variables = _device.ProfileBody.DeviceFunction.VariableCollection;

        var variable = variables.FirstOrDefault(v => v.Index == index) ?? throw new ArgumentOutOfRangeException(nameof(index));

        if (subIndex is not null && subIndex > 0)
        {
            var type = _datatypeResolver.Resolve(variable);
            var recordItem = (type as RecordT)?.Items.FirstOrDefault(rItem => rItem.Subindex == subIndex)
                ?? throw new InvalidOperationException($"{type.Id} is no Record or has no item with subindex {subIndex}");

            return _converter.Convert(_datatypeResolver.Resolve(recordItem), $"{variable.Id}_{subIndex}");
        }

        return _converter.Convert(variable);
    }
}