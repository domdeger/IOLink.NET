using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Parts.Datatypes;

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
