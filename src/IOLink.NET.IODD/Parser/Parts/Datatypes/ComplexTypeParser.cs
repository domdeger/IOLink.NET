using System.Xml.Linq;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Parts.Datatypes;

internal static class ComplexTypeParser
{
    public static ComplexDatatypeT? Parse(string typeName, XElement dataTypeElement, IParserPartLocator partLocator, byte? fixedLengthRestriction = null)
        => typeName switch
        {
            DatatypeNames.ArrayT => ArrayTParser.Parse(dataTypeElement, partLocator, fixedLengthRestriction),
            DatatypeNames.RecordT => RecordTParser.Parse(dataTypeElement, partLocator),
            _ => null
        };
}
