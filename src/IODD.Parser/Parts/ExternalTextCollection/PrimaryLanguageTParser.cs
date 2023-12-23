using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLinkNET.IODD.Parser.Parts.ExternalTextCollection;
internal static class PrimaryLanguageTParser
{
    public static PrimaryLanguageT Parse(XElement element)
    {
        string languageCode = element.ReadMandatoryAttribute("lang", XNamespace.Xml);

        return new PrimaryLanguageT(languageCode);
    }
}
