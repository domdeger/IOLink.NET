using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;

namespace IOLinkNET.IODD.Parts;

internal class TextRefTParser : IParserPart<TextRefT>
{
    private static readonly IEnumerable<XName> Targets = new[] { IODDTextRefNames.DeviceFamilyName, IODDTextRefNames.DeviceNameName,
        IODDTextRefNames.VendorTextName, IODDTextRefNames.VendorUrlName, IODDTextRefNames.Name, IODDTextRefNames.DescriptionName };

    public bool CanParse(XName name)
        => Targets.Contains(name);

    public TextRefT Parse(XElement element)
    {
        string textId = element.ReadMandatoryAttribute("textId");

        return new TextRefT(textId);
    }
}