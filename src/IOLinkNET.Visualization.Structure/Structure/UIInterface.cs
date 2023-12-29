using IOLinkNET.Integration;
using IOLinkNET.Visualization.Structure.Interfaces;

namespace IOLinkNET.Visualization.Structure.Structure;
public record UIInterface(MenuSet ObserverRoleMenu, MenuSet MaintenanceRoleMenu, MenuSet SpecialistRoleMenu, IODDPortReader IoddPortReader) : IReadable
{
    public async Task ReadAsync()
    {
        await ObserverRoleMenu.ReadAsync();
        await MaintenanceRoleMenu.ReadAsync();
        await SpecialistRoleMenu.ReadAsync();
    }
}
