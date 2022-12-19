using System.Xml.Linq;
using IOLinkNET.IODD.Structure.Profile;

namespace IOLinkNET.IODD.Parser;

internal class DeviceIdentityParser : IParserPart<DeviceIdentityT>
{
    public XName Target => IODDParserConstants.DeviceIdentityName;

    public DeviceIdentityT Parse(XElement element)
    {
        var deviceId = element.ReadMandatoryAttribute<uint>("deviceId");
        var vendorId = element.ReadMandatoryAttribute<ushort>("vendorId");
        var vendorName = element.ReadMandatoryAttribute("vendorName");
    }
}