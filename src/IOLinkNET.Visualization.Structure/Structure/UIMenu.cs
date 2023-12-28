using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.Visualization.Structure.Structure;
public record UIMenu(string Id, string? Name, ConditionT? Condition, IEnumerable<UIVariable>? Variables, IEnumerable<UIMenu>? SubMenus, IEnumerable<UIRecordItem>? RecordItems);
