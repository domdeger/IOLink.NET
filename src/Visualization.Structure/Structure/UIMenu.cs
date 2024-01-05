using IOLinkNET.Integration;
using IOLinkNET.IODD.Structure.ProcessData;
using IOLinkNET.Visualization.Structure.Interfaces;

namespace IOLinkNET.Visualization.Structure.Structure;
public record UIMenu(string Id, string? Name, ConditionT? Condition, IEnumerable<UIVariable>? Variables, IEnumerable<UIMenu>? SubMenus, IEnumerable<UIRecordItem>? RecordItems, IODDPortReader IoddPortReader) : IReadable
{
    public async Task ReadAsync()
    {
        if (Variables is not null)
        {
            foreach (UIVariable variable in Variables)
            {
                await variable.ReadAsync();
            }
        }

        if (SubMenus is not null)
        {
            foreach (UIMenu subMenu in SubMenus)
            {
                await subMenu.ReadAsync();
            }
        }

        if (RecordItems is not null)
        {
            foreach (UIRecordItem recordItem in RecordItems)
            {
                await recordItem.ReadAsync();
            }
        }
    }
}
