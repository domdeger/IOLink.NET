using IOLinkNET.IODD.Structure.ProcessData;

namespace IODD.Structure.Structure.Menu;
public record UIMenuRefT(string MenuId, ConditionT? Condition) : UIMenuRefSimpleT(MenuId);
