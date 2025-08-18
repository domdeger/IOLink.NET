using System.Xml.Linq;

using IOLink.NET.IODD.Standard.Constants;

namespace IOLink.NET.IODD.Parser.Parts.Constants;
public static class IODDExternalCollectionNames
{
    public static readonly XName PrimaryLanguageName = IODDConstants.IODDXmlNamespace.GetName("PrimaryLanguage");

    public static readonly XName TextName = IODDConstants.IODDXmlNamespace.GetName("Text");
}
