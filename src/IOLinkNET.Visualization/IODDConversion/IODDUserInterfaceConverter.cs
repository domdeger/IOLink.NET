using IOLinkNET.Integration;
using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.Structure.Menu;
using IOLinkNET.Visualization.Structure.Structure;

namespace IOLinkNET.Visualization.IODDConversion;
internal class IODDUserInterfaceConverter
{
    private readonly IODevice _ioDevice;
    private readonly IODDPortReader _ioddPortReader;
    private readonly UserInterfaceT _userInterface;

    public IODDUserInterfaceConverter(IODevice ioDevice, IODDPortReader ioddPortReader)
    {
        _ioDevice = ioDevice;
        _ioddPortReader = ioddPortReader;
        _userInterface = ioDevice.ProfileBody.DeviceFunction.UserInterface;
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
            ConvertUIMenu(observerRoleMenuSet.IdentificationMenu, StandardMenuUserRoleReader.IdentificationMenu) ?? throw new InvalidOperationException("Observerrole Menu must provide Identification Menu"),
            ConvertUIMenu(observerRoleMenuSet.ParameterMenu, StandardMenuUserRoleReader.ParameterMenu),
            ConvertUIMenu(observerRoleMenuSet.ObservationMenu, StandardMenuUserRoleReader.ObservationMenu),
            ConvertUIMenu(observerRoleMenuSet.DiagnosisMenu, StandardMenuUserRoleReader.DiagnosisMenu),
            _ioddPortReader
        );

        var convertedMaintenanceRoleMenuSet = new MenuSet(
            ConvertUIMenu(maintenanceRoleMenuSet.IdentificationMenu, StandardMenuUserRoleReader.IdentificationMenu) ?? throw new InvalidOperationException("Observerrole Menu must provide Identification Menu"),
            ConvertUIMenu(maintenanceRoleMenuSet.ParameterMenu, StandardMenuUserRoleReader.ParameterMenu),
            ConvertUIMenu(maintenanceRoleMenuSet.ObservationMenu, StandardMenuUserRoleReader.ObservationMenu),
            ConvertUIMenu(maintenanceRoleMenuSet.DiagnosisMenu, StandardMenuUserRoleReader.DiagnosisMenu),
            _ioddPortReader
        );
        var convertedSpecialistRoleMenuSet = new MenuSet(
            ConvertUIMenu(specialistRoleMenuSet.IdentificationMenu, StandardMenuUserRoleReader.IdentificationMenu) ?? throw new InvalidOperationException("Observerrole Menu must provide Identification Menu"),
            ConvertUIMenu(specialistRoleMenuSet.ParameterMenu, StandardMenuUserRoleReader.ParameterMenu),
            ConvertUIMenu(specialistRoleMenuSet.ObservationMenu, StandardMenuUserRoleReader.ObservationMenu),
            ConvertUIMenu(specialistRoleMenuSet.DiagnosisMenu, StandardMenuUserRoleReader.DiagnosisMenu),
            _ioddPortReader
        );

        return new UIInterface(convertedObserverRoleMenuSet, convertedMaintenanceRoleMenuSet, convertedSpecialistRoleMenuSet, _ioddPortReader);
    }

    private UIMenu? ConvertUIMenu(UIMenuRefSimpleT? menu, string displayNameRef)
    {
        if (menu != null && menu?.MenuId != null)
        {
            var standardMenuName = GetExternalRefText(menu, displayNameRef);
            var referencedMenu = _userInterface.MenuCollection.Where(x => x.Menu.Id == menu.MenuId).FirstOrDefault() ?? throw new InvalidOperationException("Referenced Menu not found");
            var menuName = referencedMenu.Menu.Name == string.Empty ? standardMenuName : referencedMenu.Menu.Name;

            return new UIMenu(menu.MenuId,
                menuName,
                null,
                ConvertVariableRefs(referencedMenu.Menu.VariableRefs),
                ConvertMenuRefs(referencedMenu.Menu.MenuRefs),
                ConvertRecordItemRefs(referencedMenu.Menu.RecordItemRefs),
                _ioddPortReader
            );
        }

        return null;
    }

    private UIMenu? ConvertUIMenu(UIMenuRefT? menu, string displayNameRef)
    {
        if (menu != null && menu?.MenuId != null)
        {
            var standardMenuName = GetExternalRefText(menu, displayNameRef);
            var referencedMenu = _userInterface.MenuCollection.Where(x => x.Menu.Id == menu.MenuId).FirstOrDefault() ?? throw new InvalidOperationException("Referenced Menu not found");
            var menuName = referencedMenu.Menu.Name == string.Empty ? standardMenuName : referencedMenu.Menu.Name;

            return new UIMenu(menu.MenuId,
                menuName,
                menu.Condition,
                ConvertVariableRefs(referencedMenu.Menu.VariableRefs),
                ConvertMenuRefs(referencedMenu.Menu.MenuRefs),
                ConvertRecordItemRefs(referencedMenu.Menu.RecordItemRefs),
                _ioddPortReader
            );
        }

        return null;
    }

    private List<UIVariable>? ConvertVariableRefs(IEnumerable<UIVariableRefT>? uiVariableRefs)
    {
        if (uiVariableRefs == null)
        {
            return null;
        }

        var variables = new List<UIVariable>();

        foreach (UIVariableRefT uiVariableRef in uiVariableRefs)
        {
            var variable = _ioDevice.ProfileBody.DeviceFunction.VariableCollection.Where(x => x.Id == uiVariableRef.VariableId).SingleOrDefault();

            variables.Add(new UIVariable(uiVariableRef.VariableId,
                variable,
                uiVariableRef.Gradient,
                uiVariableRef.Offset,
                uiVariableRef.UnitCode,
                uiVariableRef.AccessRights,
                uiVariableRef.ButtonValue,
                uiVariableRef.DisplayFormat,
                _ioddPortReader)
            );
        }

        return variables;
    }

    private List<UIMenu>? ConvertMenuRefs(IEnumerable<UIMenuRefT>? menuRefs)
    {
        if (menuRefs == null)
        {
            return null;
        }

        var menus = new List<UIMenu>();

        foreach (UIMenuRefT menuRef in menuRefs)
        {
            var convertedMenu = ConvertUIMenu(menuRef, string.Empty);
            if (convertedMenu != null)
            {
                menus.Add(convertedMenu);
            }
        }

        return menus;
    }

    private List<UIRecordItem>? ConvertRecordItemRefs(IEnumerable<UIRecordItemRefT>? uiRecordItemRefs)
    {
        if (uiRecordItemRefs == null)
        {
            return null;
        }

        var recordItems = new List<UIRecordItem>();

        foreach (UIRecordItemRefT itemRef in uiRecordItemRefs)
        {
            var recordItemVariable = _ioDevice.ProfileBody.DeviceFunction.VariableCollection.Where(x => x.Id == itemRef.VariableId).SingleOrDefault();

            recordItems.Add(new UIRecordItem(itemRef.VariableId,
                recordItemVariable,
                itemRef.SubIndex,
                itemRef.Gradient,
                itemRef.Offset,
                itemRef.UnitCode,
                itemRef.AccessRights,
                itemRef.ButtonValue,
                itemRef.DisplayFormat,
                _ioddPortReader)
            );
        }

        return recordItems;
    }


    private static string GetExternalRefText(UIMenuRefSimpleT? menu, string displayNameRef) =>
        menu?.Menu?.Name == string.Empty
            ? StandardMenuUserRoleReader.GetStandardMenuUserRoleText(displayNameRef, string.Empty)
            : menu?.Menu?.Name ?? string.Empty;
}
