using System.Xml.Linq;

using IODD.Structure.Structure.Menu;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Parts.Constants;

namespace IODD.Parser.Parts.Menu;
internal class MenuCollectionTParser : IParserPart<MenuCollectionT>
{
    private readonly IParserPartLocator _parserLocator;

    public MenuCollectionTParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }

    public bool CanParse(XName name) => name == IODDDeviceFunctionNames.MenuName;

    public MenuCollectionT Parse(XElement element)
    {
        MenuT menuItem = _parserLocator.Parse<MenuT>(element);
        return new MenuCollectionT(menuItem);
    }
}
