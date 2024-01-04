using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class ArrayTParser
{
    public static ArrayT Parse(XElement elem, IParserPartLocator parserLocator, byte? fixedLengthRestriction = null)
    {
        string? id = elem.ReadOptionalAttribute("id");
        byte count = fixedLengthRestriction ?? elem.ReadMandatoryAttribute<byte>("count");
        bool subindexAccessSupported = elem.ReadOptionalAttribute<bool>("subindexAccessSupported");
        DatatypeRefT? typeRef = parserLocator.ParseOptional<DatatypeRefT>(elem.Descendants(IODDParserConstants.DatatypeRefName).FirstOrDefault());

        return new ArrayT(id, count, SimpleTypeParser.Parse(elem.Descendants(IODDParserConstants.SimpleDatatypeName).FirstOrDefault()), typeRef, subindexAccessSupported);
    }
}