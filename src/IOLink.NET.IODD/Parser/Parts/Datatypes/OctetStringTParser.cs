using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Parser.Parts.Datatypes;
internal static class OctetStringTParser
{
    public static OctetStringT Parse(XElement elem, byte? fixedLengthRestriction = null)
    {
        string? id = elem.ReadOptionalAttribute("id");
        byte fixedLength = fixedLengthRestriction ?? elem.ReadMandatoryAttribute<byte>("fixedLength");

        return new OctetStringT(id, fixedLength);
    }
}
