using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using IODD.Structure.Structure.Menu;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Parts.Constants;
using IOLinkNET.IODD.Parts.Datatypes;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IODD.Parser.Parts.Menu;
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
        IEnumerable<XElement> menuElements = element.Descendants(IODDDeviceFunctionNames.MenuCollectionName).Descendants(IODDDeviceFunctionNames.MenuName);
        List<MenuCollectionT> menuCollections = new();

        foreach (var menuElement in menuElements)
        {
            MenuCollectionT menuCollection = _parserLocator.Parse<MenuCollectionT>(menuElement);
            menuCollections.Add(menuCollection);
        }

        return new UserInterfaceT(menuCollections);
    }
}
