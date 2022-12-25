using System.Xml.Linq;

using IODD.Parser.Helpers;

using IOLinkNET.IODD.Structure.Datatypes;

namespace IODD.Parser.Parts.Datatypes;

internal static class Float32TParser
{
    public static Float32T Parse(XElement elem)
    {
        string? id = elem.ReadOptionalAttribute("id");

        return new Float32T(id, Enumerable.Empty<SingleValueT<float>>(), Enumerable.Empty<ValueRangeT<float>>());
    }
}