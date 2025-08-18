using IOLink.NET.Integration;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.DeviceFunction;
using IOLink.NET.IODD.Structure.Structure.Datatypes;
using IOLink.NET.Visualization.Structure.Interfaces;

namespace IOLink.NET.Visualization.Structure.Structure;
public record UIVariable(string VariableId, VariableT? Variable, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat, IODDPortReader IoddPortReader) : IReadable
{
    public object? Value;

    public async Task ReadAsync()
    {
        if (Variable == null)
        {
            return;
        }

        Value = await IoddPortReader.ReadConvertedParameterAsync(Variable.Index, 0);
    }
}
