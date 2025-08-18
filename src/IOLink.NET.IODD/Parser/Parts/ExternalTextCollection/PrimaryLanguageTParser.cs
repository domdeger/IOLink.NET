using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLink.NET.IODD.Parser.Parts.ExternalTextCollection;
internal static class PrimaryLanguageTParser
{
    public static PrimaryLanguageT Parse(XElement element)
    {
        string languageCode = element.ReadMandatoryAttribute("lang", XNamespace.Xml);

        return new PrimaryLanguageT(languageCode);
    }
}
