using System.Xml.Linq;

using IODD.Parser.Parts;
using IODD.Structure.Structure;
using IODD.Structure.Structure.Profile;

using IOLinkNET.IODD.Parser;

namespace IODD.Parser;

public class IODDParser
{
    private readonly IParserPartLocator _partLocator = new ParserPartLocator();
    public IODDParser()
    {
        _partLocator.AddPart(new DeviceIdentityParser(_partLocator));
        _partLocator.AddPart(new TextRefTParser());
    }
    public IODevice? Parse(XElement iodd)
    {
        _ = _partLocator.Parse<DeviceIdentityT>(iodd.Descendants(IODDParserConstants.DeviceIdentityName).First());

        return null;
    }
}