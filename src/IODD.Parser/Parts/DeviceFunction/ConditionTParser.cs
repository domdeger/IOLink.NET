using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Parts.DeviceFunction;

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