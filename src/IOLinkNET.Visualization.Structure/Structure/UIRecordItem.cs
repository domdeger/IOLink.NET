using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Menu;

namespace IOLinkNET.Visualization.Structure.Structure;
public record UIRecordItem(string VariableId, byte SubIndex, decimal? Gradient, decimal? Offset, uint? UnitCode, AccessRightsT? AccessRights, string? ButtonValue, DisplayFormat? DisplayFormat) : UIDataItemRefT(VariableId, Gradient, Offset, UnitCode, AccessRights, ButtonValue, DisplayFormat);
