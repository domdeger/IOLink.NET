using System.Xml.Linq;

using IODD.Parser.Helpers;
using IODD.Parser.Parts.Constants;
using IODD.Structure.Structure.DeviceFunction;
using IODD.Structure.Structure.Profile;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.DataTypes;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IODD.Parser.Parts.DeviceFunction;

internal class DeviceFunctionTParser : IParserPart<DeviceFunctionT>
{
    private readonly IParserPartLocator _parserLocator;

    public DeviceFunctionTParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }
    public bool CanParse(XName name)
        => name == IODDParserConstants.DeviceFunctionName;

    public DeviceFunctionT Parse(XElement element)
    {
        IEnumerable<DatatypeT> dataTypeCollection = _parserLocator
                        .ParseMandatory<IEnumerable<DatatypeT>>(element.Descendants(IODDDeviceFunctionNames.DatatypeCollectionName).First()).ToArray();
        IEnumerable<VariableT> variableCollection = element.Descendants(IODDDeviceFunctionNames.VariableName)
                        .Select(_parserLocator.ParseMandatory<VariableT>).ToArray();
        IEnumerable<ProcessDataT> pdCollection = element.Descendants(IODDDeviceFunctionNames.ProcessDataCollectionName).Descendants(IODDDeviceFunctionNames.ProcessDataName)
                        .Select(_parserLocator.ParseMandatory<ProcessDataT>).ToArray();

        return new DeviceFunctionT(dataTypeCollection, variableCollection, pdCollection);
    }
}