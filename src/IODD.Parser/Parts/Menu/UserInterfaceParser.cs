﻿using System.Xml.Linq;

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
