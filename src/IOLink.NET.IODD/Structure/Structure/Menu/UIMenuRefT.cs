using IOLink.NET.IODD.Structure.ProcessData;

namespace IOLink.NET.IODD.Structure.Structure.Menu;
public record UIMenuRefT(string MenuId, ConditionT? Condition) : UIMenuRefSimpleT(MenuId, null);
