using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.DeviceFunction;

namespace IOLinkNET.IODD.Parts.DeviceFunction;

internal class RecordItemInfoParser : IParserPart<RecordItemInfoT>
{

    public RecordItemInfoParser()
    {
    }

    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.RecordItemInfo;

    public RecordItemInfoT Parse(XElement element)
    {
        byte subIndex = element.ReadMandatoryAttribute<byte>("subIndex");
        string? defaultValue = element.ReadOptionalAttribute("defaultValue");

        return new RecordItemInfoT(subIndex, defaultValue);
    }
}