using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser.Parts.Constants;

public static class IODDTextRefNames
{
    public static readonly XName VendorTextName = IODDParserConstants.IODDXmlNamespace.GetName("VendorText");

    public static readonly XName VendorUrlName = IODDParserConstants.IODDXmlNamespace.GetName("VendorUrl");

    public static readonly XName VendorLogoName = IODDParserConstants.IODDXmlNamespace.GetName("VendorLogo");

    public static readonly XName DeviceNameName = IODDParserConstants.IODDXmlNamespace.GetName("DeviceName");

    public static readonly XName DeviceFamilyName = IODDParserConstants.IODDXmlNamespace.GetName("DeviceFamily");
}