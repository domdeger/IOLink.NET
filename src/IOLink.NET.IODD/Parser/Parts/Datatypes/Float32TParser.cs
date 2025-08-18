using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;

using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Parts.Datatypes;

internal static class Float32TParser
{
    public static Float32T Parse(XElement elem)
    {
        string? id = elem.ReadOptionalAttribute("id");

        return new Float32T(id, Enumerable.Empty<SingleValueT<float>>(), Enumerable.Empty<ValueRangeT<float>>());
    }
}
