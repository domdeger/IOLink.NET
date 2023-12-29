using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Structure.Structure.ProcessData;

namespace IOLinkNET.IODD.Parser.Parts.Datatypes;
internal static class ProcessDataUnionTParser
{
    public static ProcessDataUnionT? Parse(XElement elem)
    {
        if(elem == null)
        {
            return null;
        }

        string? id = elem.ReadOptionalAttribute("id");

        return new ProcessDataUnionT(id);
    }
}
