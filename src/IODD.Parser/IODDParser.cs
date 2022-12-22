using System.Xml.Linq;

using IOLinkNET.IODD.Parser.Parts;
using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.Profile;

namespace IOLinkNET.IODD.Parser;

public class IODDParser
{
    IParserPartLocator _partLocator = new ParserPartLocator();
    public IODDParser()
    {
        _partLocator.AddPart(new DeviceIdentityParser(_partLocator));
    }
    public IODevice? Parse(XElement iodd)
    {
        var deviceIdentity = _partLocator.Parse<DeviceIdentityT>(iodd.Descendants(IODDParserConstants.DeviceIdentityName).First());

        return null;
    }
}
