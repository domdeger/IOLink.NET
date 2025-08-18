using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Structure.Structure.Datatypes;

namespace IOLink.NET.IODD.Parser.Parts.ExternalTextCollection;
internal static class TextDefinitionTParser
{
    public static TextDefinitionT Parse(XElement element)
    {
        string id = element.ReadMandatoryAttribute("id");
        string value = element.ReadMandatoryAttribute("value");

        return new TextDefinitionT(id, value);
    }
}
