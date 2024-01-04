using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parser.Parts.Datatypes;
internal static class OctetStringTParser
{
    public static OctetStringT Parse(XElement elem, byte? fixedLengthRestriction = null)
    {
        string? id = elem.ReadOptionalAttribute("id");
        byte fixedLength = fixedLengthRestriction ?? elem.ReadMandatoryAttribute<byte>("fixedLength");

        return new OctetStringT(id, fixedLength);
    }
}
