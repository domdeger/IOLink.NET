using System.Xml.Linq;
using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parser.Parts.ExternalTextCollection;
using IOLink.NET.IODD.Parser.Parts.Menu;
using IOLink.NET.IODD.Parts;
using IOLink.NET.IODD.Parts.DeviceFunction;
using IOLink.NET.IODD.Standard.Structure;
using IOLink.NET.IODD.Structure;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Profile;
using IOLink.NET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLink.NET.IODD.Parser;

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
        _partLocator.AddPart(new MenuCollectionTParser(_partLocator));
        _partLocator.AddPart(new UIMenuRefTParser(_partLocator));
        _partLocator.AddPart(new UserInterfaceParser(_partLocator));
        _partLocator.AddPart(new ExternalTextCollectionTParser());
    }

    public static bool IsIODDFile(XDocument xml)
    {
        return xml.Descendants().Any(d => d.Name.LocalName == "DeviceIdentity");
    }

    public IODevice Parse(XElement iodd)
    {
        ExternalTextCollectionT externalTextCollection =
            _partLocator.Parse<ExternalTextCollectionT>(
                iodd.Descendants(IODDParserConstants.ExternalTextCollectionName).First()
            );
        _partLocator.AddPart(new MenuElementParser(_partLocator, externalTextCollection));
        IEnumerable<DatatypeT> standardDataTypeCollection = _partLocator.ParseMandatory<
            IEnumerable<DatatypeT>
        >(StandardDefinitionReader.GetDatatypeCollection());

        DeviceIdentityT deviceIdentity = _partLocator.Parse<DeviceIdentityT>(
            iodd.Descendants(IODDParserConstants.DeviceIdentityName).First()
        );
        DeviceFunctionT deviceFunction = _partLocator.Parse<DeviceFunctionT>(
            iodd.Descendants(IODDParserConstants.DeviceFunctionName).First()
        );

        return new IODevice(
            new ProfileBodyT(deviceIdentity, deviceFunction),
            externalTextCollection,
            standardDataTypeCollection
        );
    }
}
