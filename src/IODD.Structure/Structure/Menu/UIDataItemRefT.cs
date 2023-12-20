using IODD.Structure.Structure.Datatypes;

using IOLinkNET.IODD.Structure.Datatypes;

namespace IODD.Structure.Structure.Menu;
public record UIDataItemRefT(string VariableId, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat);
