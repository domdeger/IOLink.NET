using System.Xml.Linq;

using IODD.Parser.Helpers;

using IOLinkNET.IODD.Structure.DataTypes;

namespace IODD.Parser.Parts.Datatypes;

internal static class StringTParser
{
    public static StringT Parse(XElement elem)
    {
        string id = elem.ReadMandatoryAttribute("id");
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