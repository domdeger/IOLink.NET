using IOLink.NET.IODD.Structure.Structure.Menu;

namespace IOLink.NET.IODD.Structure.Interfaces.Menu;
public interface IUserInterfaceT
{
    IEnumerable<MenuCollectionT> MenuCollection { get; }
    IMenuSetT ObserverRoleMenuSet { get; }
    IMenuSetT MaintenanceRoleMenuSet { get; }
    IMenuSetT SpecialistRoleMenuSet { get; }
}
