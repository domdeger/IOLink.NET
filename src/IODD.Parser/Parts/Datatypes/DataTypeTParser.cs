using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class DatatypeTParser
{
    public static DatatypeT Parse(XElement elem, IParserPartLocator partLocator, byte? fixedLengthRestriction = null)
    {
        string typeName = elem.ReadMandatoryAttribute("type", IODDParserConstants.XSIXmlNamespace);
        return (DatatypeT?)SimpleTypeParser.Parse(typeName, elem, fixedLengthRestriction) ?? ComplexTypeParser.Parse(typeName, elem, partLocator, fixedLengthRestriction) ?? ProcessDataUnionTypeParser.Parse(typeName, elem, partLocator)
                    ?? throw new NotSupportedException($"Could not parse data type with name {typeName}.");
    }

    public static DatatypeT? ParseOptional(XElement? element, IParserPartLocator partLocator)
        => element is not null ? Parse(element, partLocator) : null;

    public static DatatypeT? ParseOptional(XElement? element, byte fixedLengthRestriction, IParserPartLocator partLocator)
        => element is not null ? Parse(element, partLocator, fixedLengthRestriction) : null;
}