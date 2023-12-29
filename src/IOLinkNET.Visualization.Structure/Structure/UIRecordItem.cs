﻿using IOLinkNET.Integration;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Menu;
using IOLinkNET.Visualization.Structure.Interfaces;

namespace IOLinkNET.Visualization.Structure.Structure;
public record UIRecordItem(string VariableId, VariableT? Variable, byte SubIndex, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat, IODDPortReader IoddPortReader) : IReadable
{
    public object? Value;


    public async Task ReadAsync()
    {
        if (Variable == null)
        {
            return;
        }

        Value = await IoddPortReader.ReadConvertedParameterAsync(Variable.Index, SubIndex);
    }
}
