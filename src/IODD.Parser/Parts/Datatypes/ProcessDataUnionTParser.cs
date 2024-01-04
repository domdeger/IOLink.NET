using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Datatypes;
using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Structure.Datatypes;

namespace IOLinkNET.IODD.Parser.Parts.Datatypes;
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
