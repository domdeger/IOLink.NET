using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Datatypes;

namespace IOLinkNET.Visualization.Structure.Structure;
public record UIVariable(string VariableId, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat);
