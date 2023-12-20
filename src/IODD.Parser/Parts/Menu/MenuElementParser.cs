using System.Xml.Linq;

using IODD.Structure.Structure.Menu;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Parts.Constants;

namespace IODD.Parser.Parts.Menu;
internal class MenuElementParser: IParserPart<MenuT>
{
    private readonly IParserPartLocator _parserLocator;

    public MenuElementParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }

    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.MenuName;

    public MenuT Parse(XElement element)
    {
        string menuId = element.ReadMandatoryAttribute("id");
        string name = element.Descendants(IODDDeviceFunctionNames.MenuItemName).FirstOrDefault()?.ReadMandatoryAttribute("textId") ?? string.Empty;

        IEnumerable<XElement> variableRefElements = element.Descendants(IODDDeviceFunctionNames.VariableRefName);
        IEnumerable<XElement> menuRefElements = element.Descendants(IODDDeviceFunctionNames.MenuRefName);
        IEnumerable<XElement> recordItemRefElements = element.Descendants(IODDDeviceFunctionNames.RecordItemRefName);

        List<UIVariableRefT> uiVariableRefs = new();
        List<UIMenuRefT> uiMenuRefs = new();
        List<UIRecordItemRefT> uiRecordItemRefs = new();

        foreach (var variableRef in variableRefElements)
        {
            uiVariableRefs.Add(UIVariableRefTParser.Parse(variableRef));
        }

        foreach(var menuRef in menuRefElements)
        {
            UIMenuRefT? parsedMenuRef = _parserLocator.ParseOptional<UIMenuRefT>(menuRef);

            if (parsedMenuRef != null)
            {
                uiMenuRefs.Add(parsedMenuRef);
            }
        }

        foreach (var recordItemRef in recordItemRefElements)
        {
            uiRecordItemRefs.Add(UIRecordItemRefTParser.Parse(recordItemRef));
        }

        return new MenuT(menuId, name, uiVariableRefs, uiMenuRefs, uiRecordItemRefs);
    }
}
