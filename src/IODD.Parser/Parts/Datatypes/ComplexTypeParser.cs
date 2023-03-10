using System.Xml.Linq;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class ComplexTypeParser
{
    public static ComplexDatatypeT? Parse(string typeName, XElement dataTypeElement, IParserPartLocator partLocator)
        => typeName switch
        {
            DatatypeNames.ArrayT => ArrayTParser.Parse(dataTypeElement, partLocator),
            DatatypeNames.RecordT => RecordTParser.Parse(dataTypeElement, partLocator),
            _ => null
        };
}