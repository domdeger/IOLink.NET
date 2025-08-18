using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.DeviceFunction;

namespace IOLink.NET.IODD.Parts.DeviceFunction;

internal class RecordItemInfoParser : IParserPart<RecordItemInfoT>
{

    public RecordItemInfoParser()
    {
    }

    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.RecordItemInfoName;

    public RecordItemInfoT Parse(XElement element)
    {
        byte subIndex = element.ReadMandatoryAttribute<byte>("subIndex");
        string? defaultValue = element.ReadOptionalAttribute("defaultValue");

        return new RecordItemInfoT(subIndex, defaultValue);
    }
}
