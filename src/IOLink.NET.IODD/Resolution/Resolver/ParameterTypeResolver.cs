using IOLink.NET.IODD.Resolution.Contracts;
using IOLink.NET.IODD.Structure;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Resolution;

public class ParameterTypeResolver : IParameterTypeResolver
{
    private readonly IODevice _device;

    private readonly ParsableDatatypeConverter _converter;
    private readonly DatatypeResolver _datatypeResolver;

    public ParameterTypeResolver(IODevice device)
    {
        _device = device;
        _datatypeResolver = new(_device.ProfileBody.DeviceFunction.DatatypeCollection, _device.StandardDatatypeCollection);
        _converter = new(_datatypeResolver);
    }

    public ParsableDatatype GetParameter(int index, byte? subIndex = null)
    {
        var variables = _device.ProfileBody.DeviceFunction.VariableCollection;

        var variable = variables.FirstOrDefault(v => v.Index == index) ?? throw new ArgumentOutOfRangeException(nameof(index));

        if (subIndex is > 0)
        {
            var type = _datatypeResolver.Resolve(variable) as ComplexDatatypeT
                ?? throw new InvalidOperationException($"{variable.Id} is no ComplexDatatype so access via subindex is not supported.");

            if (type?.SubindexAccessSupported == true)
            {
                return type switch
                {
                    RecordT record => _converter.Convert(_datatypeResolver.Resolve(record.Items.FirstOrDefault(rItem => rItem.Subindex == subIndex)
                                        ?? throw new InvalidOperationException($"{type?.Id} has no item with subindex {subIndex}")), $"{variable.Id}_{subIndex}"),
                    ArrayT array => _converter.Convert(_datatypeResolver.Resolve(array), $"{variable.Id}_{subIndex}"),
                    _ => throw new InvalidOperationException($"{type?.Id} is an unsupported ComplexDatatype.")
                };
            }
        }

        return _converter.Convert(variable);
    }
}
