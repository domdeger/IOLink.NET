using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.ProcessData;
using IOLinkNET.IODD.Structure.Profile;

namespace IOLinkNET.IODD.Parts.DeviceFunction;

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