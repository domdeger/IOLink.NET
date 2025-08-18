using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;
using IOLink.NET.IODD.Structure.ProcessData;
using IOLink.NET.IODD.Structure.Structure.Menu;

namespace IOLink.NET.IODD.Parser.Parts.Menu;
internal class UIMenuRefTParser : IParserPart<UIMenuRefT>
{
    private readonly IParserPartLocator _parserLocator;

    public UIMenuRefTParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }

    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.MenuRefName;

    public UIMenuRefT Parse(XElement element)
    {
        string menuId = element.ReadMandatoryAttribute("menuId");

        XElement? condition = element.Elements(IODDDeviceFunctionNames.ConditionName).FirstOrDefault();
        ConditionT? conditionT = _parserLocator.ParseOptional<ConditionT>(condition);

        return new UIMenuRefT(menuId, conditionT);
    }
}
