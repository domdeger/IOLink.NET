using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using IODD.Structure.Structure.Datatypes;
using IODD.Structure.Structure.Menu;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IODD.Parser.Parts.Menu;
internal static class UIRecordItemRefTParser
{
    public static UIRecordItemRefT Parse(XElement element)
    {
        string variableId = element.ReadMandatoryAttribute("variableId");
        byte subIndex = element.ReadMandatoryAttribute<byte>("subindex");
        decimal? gradient = element.ReadOptionalAttribute("gradient") != null ? element.ReadOptionalAttribute<decimal>("gradient") : null;
        decimal? offset = element.ReadOptionalAttribute("offset") != null ? element.ReadOptionalAttribute<decimal>("offset") : null;
        uint? unitCode = element.ReadOptionalAttribute("unitCode") != null ? element.ReadOptionalAttribute<uint>("unitCode") : null;
        AccessRightsT? accessRights = AccessRightsTConverter.ParseOptional(element.ReadOptionalAttribute("accessRightRestriction") ?? string.Empty);
        string? buttonValue = element.ReadOptionalAttribute("buttonValue");
        DisplayFormat? displayFormat = DisplayFormatConverter.ParseOptional(element.ReadOptionalAttribute("displayFormat") ?? string.Empty);

        return new UIRecordItemRefT(variableId, subIndex, gradient, offset, unitCode, accessRights, buttonValue, displayFormat);
    }
}
