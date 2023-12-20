using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Datatypes;

namespace IOLinkNET.IODD.Structure.Structure.Menu;
public record UIRecordItemRefT(string VariableId, byte SubIndex, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat) : UIDataItemRefT(VariableId, Gradient, Offset, UnitCode, AccessRights, ButtonValue, DisplayFormat);
