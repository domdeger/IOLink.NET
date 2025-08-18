using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;

using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Parts.Datatypes;

internal static class BooleanTParser
{
    public static BooleanT Parse(XElement elem)
    {
        string? id = elem.ReadOptionalAttribute("id");

        return new BooleanT(id, Enumerable.Empty<SingleValueT<bool>>());
    }
}
