using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parser.Parts.Datatypes;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class ProcessDataUnionTypeParser
{
    public static DatatypeT? Parse(string typeName, XElement dataTypeElement)
        => typeName switch
        {
            DatatypeNames.ProcessDataInUnionT => ProcessDataUnionTParser.Parse(dataTypeElement),
            DatatypeNames.ProcessDataOutUnionT => ProcessDataUnionTParser.Parse(dataTypeElement),
            _ => null
        };

    public static DatatypeT? Parse(XElement? dataTypeElement)
        => dataTypeElement is null ? null : Parse(dataTypeElement.ReadMandatoryAttribute("type", IODDParserConstants.XSIXmlNamespace), dataTypeElement);

}