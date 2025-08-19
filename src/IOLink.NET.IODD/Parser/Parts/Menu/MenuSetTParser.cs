using System.Xml.Linq;
using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;
using IOLink.NET.IODD.Structure.Structure.Menu;

namespace IOLink.NET.IODD.Parser.Parts.Menu;

internal static class MenuSetTParser
{
    public static MenuSetT Parse(XElement element, IEnumerable<MenuCollectionT> menuCollections)
    {
        var identificationMenuId = element
            .Elements(IODDDeviceFunctionNames.IdentificationMenuName)
            .Single()
            .ReadMandatoryAttribute("menuId");
        var identificationMenu = menuCollections
            .Where(x => x.Menu.Id.Equals(identificationMenuId))
            .Single();

        var parameterMenuId = element
            .Elements(IODDDeviceFunctionNames.ParameterMenuName)
            .SingleOrDefault()
            ?.ReadMandatoryAttribute("menuId");
        var parameterMenu = menuCollections
            .Where(x => x.Menu.Id.Equals(parameterMenuId))
            .SingleOrDefault();

        var observationMenuId = element
            .Elements(IODDDeviceFunctionNames.ObservationMenuName)
            .SingleOrDefault()
            ?.ReadMandatoryAttribute("menuId");
        var observationMenu = menuCollections
            .Where(x => x.Menu.Id.Equals(observationMenuId))
            .SingleOrDefault();

        var diagnosisMenuId = element
            .Elements(IODDDeviceFunctionNames.DiagnosisMenuName)
            .SingleOrDefault()
            ?.ReadMandatoryAttribute("menuId");
        var diagnosisMenu = menuCollections
            .Where(x => x.Menu.Id.Equals(diagnosisMenuId))
            .SingleOrDefault();

        return new MenuSetT(
            new UIMenuRefSimpleT(identificationMenu.Menu.Id, identificationMenu.Menu),
            new UIMenuRefSimpleT(parameterMenu?.Menu.Id, parameterMenu?.Menu),
            new UIMenuRefSimpleT(observationMenu?.Menu.Id, observationMenu?.Menu),
            new UIMenuRefSimpleT(diagnosisMenu?.Menu.Id, diagnosisMenu?.Menu)
        );
    }
}
