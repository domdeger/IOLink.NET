using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;
using IOLink.NET.IODD.Structure.Structure.ExternalTextCollection;
using IOLink.NET.IODD.Structure.Structure.Menu;

namespace IOLink.NET.IODD.Parser.Parts.Menu;
internal class MenuElementParser : IParserPart<MenuT>
{
    private readonly IParserPartLocator _parserLocator;
    private readonly ExternalTextCollectionT _externalTextCollection;

    public MenuElementParser(IParserPartLocator parserLocator, ExternalTextCollectionT externalTextCollection)
    {
        _parserLocator = parserLocator;
        _externalTextCollection = externalTextCollection;
    }

    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.MenuName;

    public MenuT Parse(XElement element)
    {
        string menuId = element.ReadMandatoryAttribute("id");
        string nameTextId = element.Elements(IODDDeviceFunctionNames.MenuItemName).FirstOrDefault()?.ReadMandatoryAttribute("textId") ?? string.Empty;
        string name = _externalTextCollection?.TextDefinitions.Where(x => x.Id == nameTextId).SingleOrDefault()?.Value ?? nameTextId;

        IEnumerable<XElement> variableRefElements = element.Elements(IODDDeviceFunctionNames.VariableRefName);
        IEnumerable<XElement> menuRefElements = element.Elements(IODDDeviceFunctionNames.MenuRefName);
        IEnumerable<XElement> recordItemRefElements = element.Elements(IODDDeviceFunctionNames.RecordItemRefName);

        List<UIVariableRefT> uiVariableRefs = new();
        List<UIMenuRefT> uiMenuRefs = new();
        List<UIRecordItemRefT> uiRecordItemRefs = new();

        foreach (var variableRef in variableRefElements)
        {
            uiVariableRefs.Add(UIVariableRefTParser.Parse(variableRef));
        }

        foreach (var menuRef in menuRefElements)
        {
            UIMenuRefT? parsedMenuRef = _parserLocator.ParseOptional<UIMenuRefT>(menuRef);

            if (parsedMenuRef is not null)
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
