using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;

using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class BooleanTParser
{
    public static BooleanT Parse(XElement elem)
    {
        string? id = elem.ReadOptionalAttribute("id");

        return new BooleanT(id, Enumerable.Empty<SingleValueT<bool>>());
    }
}