using IOLink.NET.Core.Contracts;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.DeviceFunction;
using IOLink.NET.IODD.Structure.Structure.Datatypes;
using IOLink.NET.Visualization.Structure.Interfaces;

namespace IOLink.NET.Visualization.Structure.Structure;

public record UIRecordItem(
    string VariableId,
    VariableT? Variable,
    byte SubIndex,
    decimal? Gradient,
    decimal? Offset,
    uint? UnitCode,
    AccessRightsT? AccessRights,
    string? ButtonValue,
    DisplayFormat? DisplayFormat,
    IIODDPortReader IoddPortReader
) : IReadable
{
    public object? Value;

    public async Task ReadAsync(CancellationToken cancellationToken)
    {
        if (Variable == null)
        {
            return;
        }

        if (VariableId == "V_ProcessDataInput")
        {
            Value = await IoddPortReader
                .ReadConvertedProcessDataInResultAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        else if (VariableId == "V_ProcessDataOutput")
        {
            Value = await IoddPortReader
                .ReadConvertedProcessDataOutResultAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        else
        {
            Value = await IoddPortReader
                .ReadConvertedParameterResultAsync(Variable.Index, SubIndex, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
