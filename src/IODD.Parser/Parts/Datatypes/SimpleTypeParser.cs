using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parser.Parts.Datatypes;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class SimpleTypeParser
{
    public static SimpleDatatypeT? Parse(string typeName, XElement dataTypeElement, byte? fixedLengthRestriction = null)
        => typeName switch
        {
            DatatypeNames.BooleanT => BooleanTParser.Parse(dataTypeElement),
            DatatypeNames.IntegerT => IntegerTParser.ParseInt(dataTypeElement),
            DatatypeNames.UIntegerT => IntegerTParser.ParseUInt(dataTypeElement),
            DatatypeNames.StringT => StringTParser.Parse(dataTypeElement, fixedLengthRestriction),
            DatatypeNames.Float32T => Float32TParser.Parse(dataTypeElement),
            DatatypeNames.OctetStringT => OctetStringTParser.Parse(dataTypeElement, fixedLengthRestriction),
            _ => null
        };

    public static SimpleDatatypeT? Parse(XElement? dataTypeElement)
        => dataTypeElement is null ? null : Parse(dataTypeElement.ReadMandatoryAttribute("type", IODDParserConstants.XSIXmlNamespace), dataTypeElement);

}