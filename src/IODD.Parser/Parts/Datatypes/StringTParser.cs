using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;

using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class StringTParser
{
    public static StringT Parse(XElement elem)
    {
        string? id = elem.ReadOptionalAttribute("id");
        byte fixedLength = elem.ReadMandatoryAttribute<byte>("fixedLength");
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