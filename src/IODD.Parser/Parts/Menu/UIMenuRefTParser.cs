using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IODD.Structure.Structure.Datatypes;
using IODD.Structure.Structure.Menu;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Parts.Constants;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IODD.Parser.Parts.Menu;
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

        XElement? condition = element.Descendants(IODDDeviceFunctionNames.ConditionName).FirstOrDefault();
        ConditionT? conditionT = _parserLocator.ParseOptional<ConditionT>(condition);

        return new UIMenuRefT(menuId, conditionT);
    }
}
