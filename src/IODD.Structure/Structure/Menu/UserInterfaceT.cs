using IOLinkNET.IODD.Structure.Interfaces;
using IOLinkNET.IODD.Structure.Interfaces.Menu;

namespace IOLinkNET.IODD.Structure.Structure.Menu;
public record UserInterfaceT(IEnumerable<MenuCollectionT> MenuCollection, IMenuSetT ObserverRoleMenuSet, IMenuSetT MaintenanceRoleMenuSet, IMenuSetT SpecialistRoleMenuSet): IUserInterfaceT;
