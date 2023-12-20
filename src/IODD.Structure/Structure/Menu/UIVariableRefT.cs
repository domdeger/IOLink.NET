using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Datatypes;

namespace IOLinkNET.IODD.Structure.Structure.Menu;
public record UIVariableRefT(string VariableId, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat) : UIDataItemRefT(VariableId, Gradient, Offset, UnitCode, AccessRights, ButtonValue, DisplayFormat);
