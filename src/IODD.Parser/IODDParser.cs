using System.Xml.Linq;
using IOLinkNET.IODD.Structure;
namespace IOLinkNET.IODD.Parser;

public class IODDParser
{
    public IODevice? Parse(XElement iodd)
    {
        var deviceIdentity = iodd.Descendants(IODDParserConstants.DeviceIdentityName).First();
        return null;
    }
}
