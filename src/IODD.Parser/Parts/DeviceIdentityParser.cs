using System.Xml.Linq;

using IOLinkNET.IODD.Parser.Parts.Constants;
using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Profile;

namespace IOLinkNET.IODD.Parser.Parts;

internal class DeviceIdentityParser : IParserPart<DeviceIdentityT>
{
    private readonly IParserPartLocator _parserLocator;

    public DeviceIdentityParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }
    public XName Target => IODDParserConstants.DeviceIdentityName;

    public bool CanParse(XName name) => name == Target;

    public DeviceIdentityT Parse(XElement element)
    {
        var deviceId = element.ReadMandatoryAttribute<uint>("deviceId");
        var vendorId = element.ReadMandatoryAttribute<ushort>("vendorId");
        var vendorName = element.ReadMandatoryAttribute("vendorName");

        var textRefs = GetChildTextRefs(element);

        return new DeviceIdentityT(vendorId, deviceId, vendorName, textRefs.VendorName, textRefs.VendorUrl, textRefs.DeviceName, textRefs.DeviceFamilyName, textRefs.VendorLogo);
    }

    private (TextRefT DeviceName, TextRefT VendorName, TextRefT VendorUrl, TextRefT DeviceFamilyName, TextRefT? VendorLogo) GetChildTextRefs(XElement element)
    {
        var vendorTextRef = this._parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.VendorTextName).FirstOrDefault());
        var vendorUrlRef = this._parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.VendorUrlName).FirstOrDefault());

        var deviceName = this._parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.DeviceNameName).FirstOrDefault());
        var deviceFamiliy = this._parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.DeviceFamilyName).FirstOrDefault());

        var vendorLogoRef = this._parserLocator.ParseOptional<TextRefT>(element.Descendants(IODDTextRefNames.VendorLogoName).FirstOrDefault());

        return (deviceName, vendorTextRef, vendorUrlRef, deviceFamiliy, vendorLogoRef);
    }
}