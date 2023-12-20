using IODD.Structure.Structure.Datatypes;

using IOLinkNET.IODD.Structure.Datatypes;

namespace IODD.Structure.Structure.Menu;
public record UIVariableRefT(string VariableId, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat) : UIDataItemRefT(VariableId, Gradient, Offset, UnitCode, AccessRights, ButtonValue, DisplayFormat);
