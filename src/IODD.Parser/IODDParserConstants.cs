using System.Xml.Linq;

namespace IODD.Parser;

internal class IODDParserConstants
{
    public static readonly XNamespace IODDXmlNamespace = XNamespace.Get("http://www.io-link.com/IODD/2010/10");
    public static readonly XNamespace XSIXmlNamespace = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
    public static readonly XName DeviceIdentityName = IODDXmlNamespace.GetName("DeviceIdentity");

    public static readonly XName DeviceFunctionName = IODDXmlNamespace.GetName("DeviceFunction");

    public static readonly XName DatatypeCollectionName = IODDXmlNamespace.GetName("DatatypeCollection");

    public static readonly XName DatatypeName = IODDXmlNamespace.GetName("Datatype");

    public static readonly XName DatatypeRefName = IODDXmlNamespace.GetName("DatatypeRef");

    public static readonly XName SimpleDatatypeName = IODDXmlNamespace.GetName("SimpleDatatype");

    public static readonly XName SingleValueName = IODDXmlNamespace.GetName("SingleValue");
}