using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Structure.Structure.Datatypes;

namespace IOLinkNET.IODD.Parser.Parts.ExternalTextCollection;
internal static class TextDefinitionTParser
{
    public static TextDefinitionT Parse(XElement element)
    {
        string id = element.ReadMandatoryAttribute("id");
        string value = element.ReadMandatoryAttribute("value");

        return new TextDefinitionT(id, value);
    }
}
