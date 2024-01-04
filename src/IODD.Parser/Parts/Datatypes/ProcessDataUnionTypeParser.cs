using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Parser.Parts.Datatypes;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class ProcessDataUnionTypeParser
{
    public static DatatypeT? Parse(string typeName, XElement dataTypeElement, IParserPartLocator partLocator)
        => typeName switch
        {
            DatatypeNames.ProcessDataInUnionT => ProcessDataUnionTParser.Parse(dataTypeElement, partLocator),
            DatatypeNames.ProcessDataOutUnionT => ProcessDataUnionTParser.Parse(dataTypeElement, partLocator),
            _ => null
        };

    public static DatatypeT? Parse(XElement? dataTypeElement, IParserPartLocator partLocator)
        => dataTypeElement is null ? null : Parse(dataTypeElement.ReadMandatoryAttribute("type", IODDParserConstants.XSIXmlNamespace), dataTypeElement, partLocator);

}