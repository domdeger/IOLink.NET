using System.Xml.Linq;

using IODD.Parser.Helpers;
using IODD.Parser.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;

namespace IODD.Parser.Parts;

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