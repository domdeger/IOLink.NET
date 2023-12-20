using System.Xml.Linq;

using IODD.Structure.Structure.Datatypes;
using IODD.Structure.Structure.Menu;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IODD.Parser.Parts.Menu;
internal static class UIVariableRefTParser
{
    public static UIVariableRefT Parse(XElement element)
    {
        string variableId = element.ReadMandatoryAttribute("variableId");
        decimal? gradient = element.ReadOptionalAttribute("gradient") != null ? element.ReadOptionalAttribute<decimal>("gradient") : null;
        decimal? offset = element.ReadOptionalAttribute("offset") != null ? element.ReadOptionalAttribute<decimal>("offset") : null;
        uint? unitCode = element.ReadOptionalAttribute("unitCode") != null ? element.ReadOptionalAttribute<uint>("unitCode") : null;
        AccessRightsT? accessRights = AccessRightsTConverter.ParseOptional(element.ReadOptionalAttribute("accessRightRestriction") ?? string.Empty);
        string? buttonValue = element.ReadOptionalAttribute("buttonValue");
        DisplayFormat? displayFormat = DisplayFormatConverter.ParseOptional(element.ReadOptionalAttribute("buttonValue") ?? string.Empty);

        return new UIVariableRefT(variableId, gradient, offset, unitCode, accessRights, buttonValue, displayFormat);
    }
}
