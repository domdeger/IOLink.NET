using System.Xml.Linq;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.DeviceFunction;

namespace IODD.Parser.Parts;

internal class DeviceFunctionParser : IParserPart<VariableT>
{
    public static bool CanParse(XName name)
        => name == IODDParserConstants.DeviceFunctionName;

    public VariableT Parse(XElement element)
    {

    }
}