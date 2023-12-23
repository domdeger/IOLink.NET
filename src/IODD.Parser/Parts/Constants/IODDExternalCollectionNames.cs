using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser.Parts.Constants;
public static class IODDExternalCollectionNames
{
    public static readonly XName PrimaryLanguageName = IODDParserConstants.IODDXmlNamespace.GetName("PrimaryLanguage");

    public static readonly XName TextName = IODDParserConstants.IODDXmlNamespace.GetName("Text");
}
