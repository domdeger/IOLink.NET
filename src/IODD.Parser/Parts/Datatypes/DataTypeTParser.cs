using System.Xml.Linq;

using IODD.Parser.Helpers;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IODD.Parser.Parts.Datatypes;

internal static class DatatypeTParser
{
    public static DatatypeT Parse(XElement elem, IParserPartLocator partLocator)
    {
        string typeName = elem.ReadMandatoryAttribute("type", IODDParserConstants.XSIXmlNamespace);
        return (DatatypeT?)SimpleTypeParser.Parse(typeName, elem) ?? ComplexTypeParser.Parse(typeName, elem, partLocator)
                    ?? throw new NotSupportedException($"Could not parse data type with name {typeName}.");
    }

    public static DatatypeT? ParseOptional(XElement? element, IParserPartLocator partLocator)
        => element is not null ? Parse(element, partLocator) : null;
}