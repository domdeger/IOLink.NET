using System.Xml.Linq;

using IOLink.NET.IODD.Parts.Constants;
using IOLink.NET.IODD.Structure.Structure.Menu;

namespace IOLink.NET.IODD.Parser.Parts.Menu;
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
