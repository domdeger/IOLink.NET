using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;
using IOLinkNET.IODD.Structure.Structure.Menu;

namespace IOLinkNET.IODD.Parser.Parts.Menu;
internal static class MenuSetTParser
{
    public static MenuSetT Parse(XElement element, IEnumerable<MenuCollectionT> menuCollections)
    {
        var identificationMenuId = element.Elements(IODDDeviceFunctionNames.IdentificationMenuName).Single().ReadMandatoryAttribute("menuId");
        var identificationMenu = menuCollections.Where(x => x.Menu.Id.Equals(identificationMenuId)).Single();

        var parameterMenuId = element.Elements(IODDDeviceFunctionNames.ParameterMenuName).Single().ReadMandatoryAttribute("menuId");
        var parameterMenu = menuCollections.Where(x => x.Menu.Id.Equals(parameterMenuId)).SingleOrDefault();

        var observationMenuId = element.Elements(IODDDeviceFunctionNames.ObservationMenuName).Single().ReadMandatoryAttribute("menuId");
        var observationMenu = menuCollections.Where(x => x.Menu.Id.Equals(observationMenuId)).SingleOrDefault();

        var diagnosisMenuId = element.Elements(IODDDeviceFunctionNames.DiagnosisMenuName).Single().ReadMandatoryAttribute("menuId");
        var diagnosisMenu = menuCollections.Where(x => x.Menu.Id.Equals(diagnosisMenuId)).SingleOrDefault();

        return new MenuSetT(new UIMenuRefSimpleT(identificationMenu.Menu.Id, identificationMenu.Menu), 
            new UIMenuRefSimpleT(parameterMenu?.Menu.Id, parameterMenu?.Menu), 
            new UIMenuRefSimpleT(observationMenu?.Menu.Id, observationMenu?.Menu), 
            new UIMenuRefSimpleT(diagnosisMenu?.Menu.Id, diagnosisMenu?.Menu)
        );
    }
}
