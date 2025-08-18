using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Structure.Datatypes;

namespace IOLink.NET.IODD.Structure.Structure.Menu;
public record UIVariableRefT(string VariableId, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat) : UIDataItemRefT(VariableId, Gradient, Offset, UnitCode, AccessRights, ButtonValue, DisplayFormat);
