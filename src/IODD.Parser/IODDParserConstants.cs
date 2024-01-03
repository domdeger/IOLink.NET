using System.Xml.Linq;

using IOLinkNET.IODD.Standard.Constants;

namespace IOLinkNET.IODD;

internal class IODDParserConstants
{    
    public static readonly XNamespace XSIXmlNamespace = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
    public static readonly XName DeviceIdentityName = IODDConstants.IODDXmlNamespace.GetName("DeviceIdentity");

    public static readonly XName DeviceFunctionName = IODDConstants.IODDXmlNamespace.GetName("DeviceFunction");

    public static readonly XName ExternalTextCollectionName = IODDConstants.IODDXmlNamespace.GetName("ExternalTextCollection");

    public static readonly XName DatatypeCollectionName = IODDConstants.IODDXmlNamespace.GetName("DatatypeCollection");

    public static readonly XName DatatypeName = IODDConstants.IODDXmlNamespace.GetName("Datatype");

    public static readonly XName DatatypeRefName = IODDConstants.IODDXmlNamespace.GetName("DatatypeRef");

    public static readonly XName SimpleDatatypeName = IODDConstants.IODDXmlNamespace.GetName("SimpleDatatype");

    public static readonly XName SingleValueName = IODDConstants.IODDXmlNamespace.GetName("SingleValue");
}