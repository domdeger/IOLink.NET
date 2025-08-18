using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.DeviceFunction;
using IOLink.NET.IODD.Structure.ProcessData;
using IOLink.NET.IODD.Structure.Profile;
using IOLink.NET.IODD.Structure.Structure.Menu;

namespace IOLink.NET.IODD.Parts.DeviceFunction;

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
        var dataTypeCollection = _parserLocator
            .ParseOptional<IEnumerable<DatatypeT>>(element
                .Descendants(IODDDeviceFunctionNames.DatatypeCollectionName)
                .FirstOrDefault()
            )?
            .ToArray() ?? Array.Empty<DatatypeT>();
        
        var variableCollection = element
            .Descendants(IODDDeviceFunctionNames.VariableName)
            .Select(_parserLocator.ParseMandatory<VariableT>)
            .Concat(element
            .Descendants(IODDDeviceFunctionNames.StandardVariableRefName)
            .Select(_standardVariableRefTParser.Parse)
            .ToArray());

        var pdCollection = element
            .Descendants(IODDDeviceFunctionNames.ProcessDataCollectionName)
            .Descendants(IODDDeviceFunctionNames.ProcessDataName)
            .Select(_parserLocator.ParseMandatory<ProcessDataT>)
            .ToArray();

        var userInterface = element
            .Descendants(IODDDeviceFunctionNames.UserInterfaceName)
            .Select(_parserLocator.ParseMandatory<UserInterfaceT>)
            .First();

        return new DeviceFunctionT(dataTypeCollection, variableCollection, pdCollection, userInterface);
    }
}
