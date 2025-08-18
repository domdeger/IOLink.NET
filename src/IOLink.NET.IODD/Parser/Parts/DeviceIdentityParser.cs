using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Profile;

namespace IOLink.NET.IODD.Parts;

internal class DeviceIdentityParser : IParserPart<DeviceIdentityT>
{
    private readonly IParserPartLocator _parserLocator;

    public DeviceIdentityParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }
    public static XName Target => IODDParserConstants.DeviceIdentityName;

    public bool CanParse(XName name) => name == Target;

    public DeviceIdentityT Parse(XElement element)
    {
        uint deviceId = element.ReadMandatoryAttribute<uint>("deviceId");
        ushort vendorId = element.ReadMandatoryAttribute<ushort>("vendorId");
        string vendorName = element.ReadMandatoryAttribute("vendorName");
        (TextRefT deviceName, TextRefT vendorText, TextRefT vendorUrl, TextRefT deviceFamilyName) = GetChildTextRefs(element);

        return new DeviceIdentityT(vendorId, deviceId, vendorName, vendorText, vendorUrl, deviceName, deviceFamilyName);
    }

    private (TextRefT DeviceName, TextRefT VendorName, TextRefT VendorUrl, TextRefT DeviceFamilyName) GetChildTextRefs(XElement element)
    {
        TextRefT vendorTextRef = _parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.VendorTextName).FirstOrDefault());
        TextRefT vendorUrlRef = _parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.VendorUrlName).FirstOrDefault());

        TextRefT deviceName = _parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.DeviceNameName).FirstOrDefault());
        TextRefT deviceFamiliy = _parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.DeviceFamilyName).FirstOrDefault());

        return (deviceName, vendorTextRef, vendorUrlRef, deviceFamiliy);
    }
}
