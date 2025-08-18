using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.ProcessData;

namespace IOLink.NET.IODD.Parts.DeviceFunction;

internal class ConditionTParser : IParserPart<ConditionT>
{
    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.ConditionName;

    public ConditionT Parse(XElement element)
    {
        string variableId = element.ReadMandatoryAttribute("variableId");
        byte subIndex = element.ReadOptionalAttribute<byte>("subIndex");
        int value = element.ReadMandatoryAttribute<int>("value");
        return new ConditionT(variableId, subIndex, value);
    }
}
