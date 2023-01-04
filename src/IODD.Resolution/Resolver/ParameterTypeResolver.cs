using IODD.Resolution.Model;

using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IODD.Resolution;

public class ParameterTypeResolver
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
        var type = _datatypeResolver.Resolve(variable);

        if (subIndex is not null)
        {
            var recordItem = (type as RecordT)?.Items.FirstOrDefault(rItem => rItem.Subindex == subIndex) 
                ?? throw new InvalidOperationException($"{type.Id} is no Record or has no item with subindex {subIndex}");
            
            return _converter.Convert(_datatypeResolver.Resolve(recordItem), recordItem.Name.TextId);
        }

        return _converter.Convert(type);
    }
}