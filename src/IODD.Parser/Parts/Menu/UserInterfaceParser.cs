using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;
using IOLinkNET.IODD.Structure.Structure.Menu;

namespace IOLinkNET.IODD.Parser.Parts.Menu;
internal class UserInterfaceParser : IParserPart<UserInterfaceT>
{
    private readonly IParserPartLocator _parserLocator;

    public UserInterfaceParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }

    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.UserInterfaceName;


    public UserInterfaceT Parse(XElement element)
    {
        IEnumerable<XElement> menuElements = element.Elements(IODDDeviceFunctionNames.MenuCollectionName).Elements(IODDDeviceFunctionNames.MenuName);
        List<MenuCollectionT> menuCollections = new();

        foreach (var menuElement in menuElements)
        {
            MenuCollectionT menuCollection = _parserLocator.Parse<MenuCollectionT>(menuElement);
            menuCollections.Add(menuCollection);
        }

        XElement observerRoleMenuSetElement = element.Elements(IODDDeviceFunctionNames.ObserverRoleMenuSetName).First();
        MenuSetT observerRoleMenu = MenuSetTParser.Parse(observerRoleMenuSetElement, menuCollections);

        XElement maintenanceRoleMenuSetElement = element.Elements(IODDDeviceFunctionNames.MaintenanceRoleMenuSetName).First();
        MenuSetT maintenanceRoleMenu = MenuSetTParser.Parse(maintenanceRoleMenuSetElement, menuCollections);

        XElement specialistRoleMenuSetElement = element.Elements(IODDDeviceFunctionNames.SpecialistRoleMenuSetName).First();
        MenuSetT specialistRoleMenu = MenuSetTParser.Parse(specialistRoleMenuSetElement, menuCollections);

        return new UserInterfaceT(menuCollections, observerRoleMenu, maintenanceRoleMenu, specialistRoleMenu);
    }
}
