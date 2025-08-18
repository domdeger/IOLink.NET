using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.Common;

namespace IOLink.NET.IODD.Parts;

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
