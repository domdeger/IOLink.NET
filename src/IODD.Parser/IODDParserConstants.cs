using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser;

internal class IODDParserConstants
{
    public static readonly XNamespace IODDXmlNamespace = XNamespace.Get("http://www.io-link.com/IODD/2010/10");
    public static readonly XName DeviceIdentityName = IODDXmlNamespace.GetName("DeviceIdentity");
    public static readonly XName DatatypeCollectionName = IODDXmlNamespace.GetName("DatatypeCollection");
    public static readonly XName DatatypeName = IODDXmlNamespace.GetName("DatatypeName");
    public static readonly XName SingleValueName = IODDXmlNamespace.GetName("SingleValue");
}