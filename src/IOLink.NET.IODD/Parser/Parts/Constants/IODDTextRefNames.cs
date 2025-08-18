using System.Xml.Linq;

using IOLink.NET.IODD.Standard.Constants;

namespace IOLink.NET.IODD.Parts.Constants;

public static class IODDTextRefNames
{
    public static readonly XName VendorTextName = IODDConstants.IODDXmlNamespace.GetName("VendorText");

    public static readonly XName VendorUrlName = IODDConstants.IODDXmlNamespace.GetName("VendorUrl");

    public static readonly XName VendorLogoName = IODDConstants.IODDXmlNamespace.GetName("VendorLogo");

    public static readonly XName DeviceNameName = IODDConstants.IODDXmlNamespace.GetName("DeviceName");

    public static readonly XName DeviceFamilyName = IODDConstants.IODDXmlNamespace.GetName("DeviceFamily");

    public static readonly XName Name = IODDConstants.IODDXmlNamespace.GetName("Name");

    public static readonly XName DescriptionName = IODDConstants.IODDXmlNamespace.GetName("Description");
}
