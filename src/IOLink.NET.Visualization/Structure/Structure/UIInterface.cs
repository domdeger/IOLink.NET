using IOLink.NET.Core.Contracts;
using IOLink.NET.Visualization.Structure.Interfaces;

namespace IOLink.NET.Visualization.Structure.Structure;

public record UIInterface(
    MenuSet ObserverRoleMenu,
    MenuSet MaintenanceRoleMenu,
    MenuSet SpecialistRoleMenu,
    IIODDPortReader IoddPortReader
) : IReadable
{
    public async Task ReadAsync()
    {
        await ObserverRoleMenu.ReadAsync();
        await MaintenanceRoleMenu.ReadAsync();
        await SpecialistRoleMenu.ReadAsync();
    }
}
