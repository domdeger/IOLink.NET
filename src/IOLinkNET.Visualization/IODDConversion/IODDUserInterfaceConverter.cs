using IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;
using IOLinkNET.IODD.Structure.Structure.Menu;
using IOLinkNET.Visualization.Structure.Structure;

namespace IOLinkNET.Visualization.IODDConversion;
internal class IODDUserInterfaceConverter
{
    private readonly UserInterfaceT _userInterface;

    public IODDUserInterfaceConverter(UserInterfaceT userInterface)
    {
        _userInterface = userInterface;
    }

    public UIInterface Convert()
    {
        if (_userInterface == null)
        {
            throw new ArgumentNullException("User Interface not present in IODD");
        }

        var observerRoleMenuSet = _userInterface.ObserverRoleMenuSet;
        var maintenanceRoleMenuSet = _userInterface.MaintenanceRoleMenuSet;
        var specialistRoleMenuSet = _userInterface.SpecialistRoleMenuSet;

        var convertedObserverRoleMenuSet = new MenuSet(
            ConvertUIMenu(observerRoleMenuSet.IdentificationMenu, MenuUserRoleDefinition.IdentificationMenu) ?? throw new InvalidOperationException("Observerrole Menu must provide Identification Menu"), 
            ConvertUIMenu(observerRoleMenuSet.ParameterMenu, MenuUserRoleDefinition.ParameterMenu), 
            ConvertUIMenu(observerRoleMenuSet.ObservationMenu, MenuUserRoleDefinition.ObservationMenu), 
            ConvertUIMenu(observerRoleMenuSet.DiagnosisMenu, MenuUserRoleDefinition.DiagnosisMenu)
        );

        var convertedMaintenanceRoleMenuSet = new MenuSet(
            ConvertUIMenu(maintenanceRoleMenuSet.IdentificationMenu, MenuUserRoleDefinition.IdentificationMenu) ?? throw new InvalidOperationException("Observerrole Menu must provide Identification Menu"),
            ConvertUIMenu(maintenanceRoleMenuSet.ParameterMenu, MenuUserRoleDefinition.ParameterMenu),
            ConvertUIMenu(maintenanceRoleMenuSet.ObservationMenu, MenuUserRoleDefinition.ObservationMenu),
            ConvertUIMenu(maintenanceRoleMenuSet.DiagnosisMenu, MenuUserRoleDefinition.DiagnosisMenu)
        );
        var convertedSpecialistRoleMenuSet = new MenuSet(
            ConvertUIMenu(specialistRoleMenuSet.IdentificationMenu, MenuUserRoleDefinition.IdentificationMenu) ?? throw new InvalidOperationException("Observerrole Menu must provide Identification Menu"),
            ConvertUIMenu(specialistRoleMenuSet.ParameterMenu, MenuUserRoleDefinition.ParameterMenu),
            ConvertUIMenu(specialistRoleMenuSet.ObservationMenu, MenuUserRoleDefinition.ObservationMenu),
            ConvertUIMenu(specialistRoleMenuSet.DiagnosisMenu, MenuUserRoleDefinition.DiagnosisMenu)
        );

        return new UIInterface(convertedObserverRoleMenuSet, convertedMaintenanceRoleMenuSet, convertedSpecialistRoleMenuSet);
    }

    private UIMenu? ConvertUIMenu(UIMenuRefSimpleT? menu, string displayNameRef)
    {
        if (menu != null && menu?.MenuId != null)
        {
            var stdMenuName = GetExternalRefText(menu, displayNameRef);
            var referencedMenu = _userInterface.MenuCollection.Where(x => x.Menu.Id == menu.MenuId).FirstOrDefault() ?? throw new InvalidOperationException("Referenced Menu not found");
            var menuName = referencedMenu.Menu.Name == string.Empty ? stdMenuName : referencedMenu.Menu.Name;

            return new UIMenu(menu.MenuId, menuName, null, ConvertVariableRefs(referencedMenu.Menu.VariableRefs), ConvertMenuRefs(referencedMenu.Menu.MenuRefs), ConvertRecordItemRefs(referencedMenu.Menu.RecordItemRefs));
        }

        return null;
    }

    private UIMenu? ConvertUIMenu(UIMenuRefT? menu, string displayNameRef)
    {
        if (menu != null && menu?.MenuId != null)
        {
            var stdMenuName = GetExternalRefText(menu, displayNameRef);
            var referencedMenu = _userInterface.MenuCollection.Where(x => x.Menu.Id == menu.MenuId).FirstOrDefault() ?? throw new InvalidOperationException("Referenced Menu not found");
            var menuName = referencedMenu.Menu.Name == string.Empty ? stdMenuName : referencedMenu.Menu.Name;

            return new UIMenu(menu.MenuId, menuName, menu.Condition, ConvertVariableRefs(referencedMenu.Menu.VariableRefs), ConvertMenuRefs(referencedMenu.Menu.MenuRefs), ConvertRecordItemRefs(referencedMenu.Menu.RecordItemRefs));
        }

        return null;
    }

    private IEnumerable<UIVariable>? ConvertVariableRefs(IEnumerable<UIVariableRefT>? uiVariableRefs)
    {
        if (uiVariableRefs == null)
        {
            return null;
        }

        var list = new List<UIVariable>();

        foreach (var uiVariableRef in uiVariableRefs)
        {
            list.Add(new UIVariable(uiVariableRef.VariableId, 
                uiVariableRef.Gradient, 
                uiVariableRef.Offset, 
                uiVariableRef.UnitCode, 
                uiVariableRef.AccessRights, 
                uiVariableRef.ButtonValue, 
                uiVariableRef.DisplayFormat)
            );
        }

        return list;
    }

    private IEnumerable<UIMenu>? ConvertMenuRefs(IEnumerable<UIMenuRefT>? menuRefs)
    {
        if (menuRefs == null)
        {
            return null;
        }

        var list = new List<UIMenu>();

        foreach (var menuRef in menuRefs)
        {
            var convertedMenu = ConvertUIMenu(menuRef, string.Empty);
            if (convertedMenu != null)
            {
                list.Add(convertedMenu);
            }
        }

        return list;
    }

    private IEnumerable<UIRecordItem>? ConvertRecordItemRefs(IEnumerable<UIRecordItemRefT>? uiRecordItemRefs)
    {
        if (uiRecordItemRefs == null)
        {
            return null;
        }

        var list = new List<UIRecordItem>();

        foreach (var itemRef in uiRecordItemRefs)
        {
            list.Add(new UIRecordItem(itemRef.VariableId, 
                itemRef.SubIndex, 
                itemRef.Gradient, 
                itemRef.Offset, 
                itemRef.UnitCode, 
                itemRef.AccessRights, 
                itemRef.ButtonValue, 
                itemRef.DisplayFormat)
            );
        }

        return list;
    }


    private string GetExternalRefText(UIMenuRefSimpleT? menu, string displayNameRef)
    {
        if (menu?.Menu?.Name == string.Empty) {
            return MenuUserRoleDefinition.GetTranslatedText(displayNameRef, string.Empty);
        }

        return menu?.Menu?.Name ?? string.Empty;
    }
}
