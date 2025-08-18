using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;

using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Parts.Datatypes;

internal static class StringTParser
{
    public static StringT Parse(XElement elem, byte? fixedLengthRestriction = null)
    {
        string? id = elem.ReadOptionalAttribute("id");
        byte fixedLength = fixedLengthRestriction ?? elem.ReadMandatoryAttribute<byte>("fixedLength");
        string encoding = elem.ReadMandatoryAttribute("encoding");

        return new StringT(id, fixedLength, ParseEncoding(encoding));
    }

    private static StringTEncoding ParseEncoding(string value) => value switch
    {
        "UTF-8" => StringTEncoding.UTF8,
        "ASCII" => StringTEncoding.ASCII,
        _ => throw new NotImplementedException("")
    };
}
