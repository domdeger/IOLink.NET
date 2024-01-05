using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Menu;

namespace IOLinkNET.IODD.Parser.Parts.Menu;
internal static class UIRecordItemRefTParser
{
    public static UIRecordItemRefT Parse(XElement element)
    {
        string variableId = element.ReadMandatoryAttribute("variableId");
        byte subIndex = element.ReadMandatoryAttribute<byte>("subindex");
        decimal? gradient = element.ReadOptionalAttribute("gradient") is not null ? element.ReadOptionalAttribute<decimal>("gradient") : null;
        decimal? offset = element.ReadOptionalAttribute("offset") is not null ? element.ReadOptionalAttribute<decimal>("offset") : null;
        uint? unitCode = element.ReadOptionalAttribute("unitCode") is not null ? element.ReadOptionalAttribute<uint>("unitCode") : null;
        AccessRightsT? accessRights = AccessRightsTConverter.ParseOptional(element.ReadOptionalAttribute("accessRightRestriction") ?? string.Empty);
        string? buttonValue = element.ReadOptionalAttribute("buttonValue");
        DisplayFormat? displayFormat = DisplayFormatConverter.ParseOptional(element.ReadOptionalAttribute("displayFormat") ?? string.Empty);

        return new UIRecordItemRefT(variableId, subIndex, gradient, offset, unitCode, accessRights, buttonValue, displayFormat);
    }
}
