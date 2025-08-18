using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Datatypes;
using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Structure.Datatypes;

namespace IOLink.NET.IODD.Parser.Parts.Datatypes;
internal static class ProcessDataUnionTParser
{
    public static ProcessDataUnionT? Parse(XElement elem, IParserPartLocator parserLocator)
    {
        if(elem == null)
        {
            return null;
        }

        string? id = elem.ReadOptionalAttribute("id");
        DatatypeRefT? typeRef = parserLocator.ParseOptional<DatatypeRefT>(elem.Descendants(IODDParserConstants.DatatypeRefName).FirstOrDefault());

        return new ProcessDataUnionT(id, SimpleTypeParser.Parse(elem.Descendants(IODDParserConstants.SimpleDatatypeName).FirstOrDefault()), typeRef);
    }
}
