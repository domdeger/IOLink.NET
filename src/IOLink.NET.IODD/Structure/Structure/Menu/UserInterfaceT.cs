using IOLink.NET.IODD.Structure.Interfaces.Menu;

namespace IOLink.NET.IODD.Structure.Structure.Menu;

public record UserInterfaceT(
    IEnumerable<MenuCollectionT> MenuCollection,
    IMenuSetT ObserverRoleMenuSet,
    IMenuSetT MaintenanceRoleMenuSet,
    IMenuSetT SpecialistRoleMenuSet
) : IUserInterfaceT;
