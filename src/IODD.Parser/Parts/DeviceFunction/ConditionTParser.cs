using System.Xml.Linq;

using IODD.Parser.Helpers;
using IODD.Parser.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IODD.Parser.Parts.DeviceFunction;

internal class ConditionTParser : IParserPart<ConditionT>
{
    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.ConditionName;

    public ConditionT Parse(XElement element)
    {
        string variableId = element.ReadMandatoryAttribute("variableId");
        byte subIndex = element.ReadMandatoryAttribute<byte>("subIndex");
        byte value = element.ReadMandatoryAttribute<byte>("value");
        return new ConditionT(variableId, subIndex, value);
    }
}