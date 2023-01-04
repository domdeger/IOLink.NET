using System.Xml.Linq;

using IOLinkNET.IODD.Parts;
using IOLinkNET.IODD.Parts.DeviceFunction;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.Profile;

namespace IOLinkNET.IODD;

public class IODDParser
{
    private readonly IParserPartLocator _partLocator = new ParserPartLocator();
    public IODDParser()
    {
        _partLocator.AddPart(new DeviceIdentityParser(_partLocator));
        _partLocator.AddPart(new TextRefTParser());
        _partLocator.AddPart(new DatatypeRefTParser());
        _partLocator.AddPart(new DatatypeCollectionTParser(_partLocator));
        _partLocator.AddPart(new ProcessDataParser(_partLocator));
        _partLocator.AddPart(new ProcessDataItemParser(_partLocator));
        _partLocator.AddPart(new ConditionTParser());
        _partLocator.AddPart(new DeviceFunctionTParser(_partLocator));
        _partLocator.AddPart(new VariableTParser(_partLocator));
        _partLocator.AddPart(new DatatypeCollectionTParser(_partLocator));
    }
    public IODevice? Parse(XElement iodd)
    {
        DeviceIdentityT deviceIdentity = _partLocator.Parse<DeviceIdentityT>(iodd.Descendants(IODDParserConstants.DeviceIdentityName).First());
        DeviceFunctionT deviceFunction = _partLocator.Parse<DeviceFunctionT>(iodd.Descendants(IODDParserConstants.DeviceFunctionName).First());

        return new IODevice(new ProfileBodyT(deviceIdentity, deviceFunction));
    }
}