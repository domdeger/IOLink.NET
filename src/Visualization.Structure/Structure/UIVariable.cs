using IOLinkNET.Integration;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.Structure.Datatypes;
using IOLinkNET.Visualization.Structure.Interfaces;

namespace IOLinkNET.Visualization.Structure.Structure;
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
