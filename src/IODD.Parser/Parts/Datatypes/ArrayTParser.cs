using System.Xml.Linq;

using IODD.Parser.Helpers;
using IODD.Structure.Structure.Datatypes;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;

namespace IODD.Parser.Parts.Datatypes;

internal static class ArrayTParser
{
    public static ArrayT Parse(XElement elem, IParserPartLocator parserLocator)
    {
        string? id = elem.ReadOptionalAttribute("id");
        byte count = elem.ReadMandatoryAttribute<byte>("count");
        DatatypeRefT? typeRef = parserLocator.ParseOptional<DatatypeRefT>(elem.Descendants(IODDParserConstants.DatatypeRefName).FirstOrDefault());

        return new ArrayT(id, count, SimpleTypeParser.Parse(elem.Descendants(IODDParserConstants.SimpleDatatypeName).FirstOrDefault()), typeRef);
    }
}