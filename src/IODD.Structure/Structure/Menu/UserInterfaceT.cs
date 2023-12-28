namespace IOLinkNET.IODD.Structure.Structure.Menu;
public record UserInterfaceT(IEnumerable<MenuCollectionT> MenuCollection, MenuSetT ObserverRoleMenuSet, MenuSetT MaintenanceRoleMenuSet, MenuSetT SpecialistRoleMenuSet);
