using System.Xml.Linq;

using IODD.Structure.Structure.Profile;

using IOLinkNET.IODD.Parser;

namespace IODD.Parser.Parts.DeviceFunction;

internal class DeviceFunctionParser : IParserPart<DeviceFunctionT>
{
    public bool CanParse(XName name)
        => name == IODDParserConstants.DeviceFunctionName;

    public DeviceFunctionT Parse(XElement element)
    {

    }
}