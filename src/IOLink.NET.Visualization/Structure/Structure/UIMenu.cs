using IOLink.NET.Core.Contracts;
using IOLink.NET.IODD.Structure.ProcessData;
using IOLink.NET.Visualization.Structure.Interfaces;

namespace IOLink.NET.Visualization.Structure.Structure;

public record UIMenu(
    string Id,
    string? Name,
    ConditionT? Condition,
    IEnumerable<UIVariable>? Variables,
    IEnumerable<UIMenu>? SubMenus,
    IEnumerable<UIRecordItem>? RecordItems,
    IIODDPortReader IoddPortReader
) : IReadable
{
    public async Task ReadAsync(CancellationToken cancellationToken)
    {
        if (Variables is not null)
        {
            foreach (UIVariable variable in Variables)
            {
                await variable.ReadAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        if (SubMenus is not null)
        {
            foreach (UIMenu subMenu in SubMenus)
            {
                await subMenu.ReadAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        if (RecordItems is not null)
        {
            foreach (UIRecordItem recordItem in RecordItems)
            {
                await recordItem.ReadAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
