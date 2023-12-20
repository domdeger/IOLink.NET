using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;
using IOLinkNET.IODD.Structure.ProcessData;
using IOLinkNET.IODD.Structure.Structure.Menu;

namespace IOLinkNET.IODD.Parser.Parts.Menu;
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
