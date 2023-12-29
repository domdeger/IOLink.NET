using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.ProcessData;
using IOLinkNET.IODD.Structure.Profile;
using IOLinkNET.IODD.Structure.Structure.Menu;

namespace IOLinkNET.IODD.Parts.DeviceFunction;

internal class DeviceFunctionTParser : IParserPart<DeviceFunctionT>
{
    private readonly IParserPartLocator _parserLocator;
    private readonly StandardVariableRefTParser _standardVariableRefTParser;

    public DeviceFunctionTParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
        _standardVariableRefTParser = new(parserLocator);
    }
    public bool CanParse(XName name)
        => name == IODDParserConstants.DeviceFunctionName;

    public DeviceFunctionT Parse(XElement element)
    {
        IEnumerable<DatatypeT> dataTypeCollection = _parserLocator
            .ParseOptional<IEnumerable<DatatypeT>>(element
                .Descendants(IODDDeviceFunctionNames.DatatypeCollectionName)
                .FirstOrDefault()
            )?
            .ToArray() ?? Array.Empty<DatatypeT>();
        
        List<VariableT> variableCollection = element
            .Descendants(IODDDeviceFunctionNames.VariableName)
            .Select(_parserLocator.ParseMandatory<VariableT>)
            .ToList();
        variableCollection.AddRange(element
            .Descendants(IODDDeviceFunctionNames.StandardVariableRefName)
            .Select(_standardVariableRefTParser.Parse)
            .ToList());

        IEnumerable<ProcessDataT> pdCollection = element
            .Descendants(IODDDeviceFunctionNames.ProcessDataCollectionName)
            .Descendants(IODDDeviceFunctionNames.ProcessDataName)
            .Select(_parserLocator.ParseMandatory<ProcessDataT>)
            .ToArray();

        UserInterfaceT userInterface = element
            .Descendants(IODDDeviceFunctionNames.UserInterfaceName)
            .Select(_parserLocator.ParseMandatory<UserInterfaceT>)
            .First();

        return new DeviceFunctionT(dataTypeCollection, variableCollection, pdCollection, userInterface);
    }
}