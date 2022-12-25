using System.Xml.Linq;

using IODD.Parser.Helpers;

using IOLinkNET.IODD.Structure.Datatypes;

namespace IODD.Parser.Parts.Datatypes;

internal static class BooleanTParser
{
    public static BooleanT Parse(XElement elem)
    {
        string? id = elem.ReadOptionalAttribute("id");

        return new BooleanT(id, Enumerable.Empty<SingleValueT<bool>>());
    }
}