using System.Xml.Linq;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Profile;

namespace IODD.Parser.Parts;

internal class DeviceFunctionParser : IParserPart<DeviceFunctionT>
{
    public bool CanParse(XName name)
        => name == IODDParserConstants.DeviceFunctionName;

    public DeviceFunctionT Parse(XElement element)
    {

    }
}