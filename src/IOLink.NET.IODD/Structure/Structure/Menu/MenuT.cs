namespace IOLink.NET.IODD.Structure.Structure.Menu;
public record MenuT(string Id, string? Name, IEnumerable<UIVariableRefT>? VariableRefs, IEnumerable<UIMenuRefT>? MenuRefs, IEnumerable<UIRecordItemRefT>? RecordItemRefs);
