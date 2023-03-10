using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Parts.DeviceFunction;

internal class ProcessDataParser : IParserPart<ProcessDataT>
{
    private readonly IParserPartLocator _parserLocator;

    public ProcessDataParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }

    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.ProcessDataName;

    public ProcessDataT Parse(XElement element)
    {
        ConditionT? condition = _parserLocator.ParseOptional<ConditionT>(element.Descendants(IODDDeviceFunctionNames.ConditionName).FirstOrDefault());
        ProcessDataItemT? pdin = _parserLocator.ParseOptional<ProcessDataItemT>(element.Descendants(IODDDeviceFunctionNames.ProcessDataInName).FirstOrDefault());
        ProcessDataItemT? pdout = _parserLocator.ParseOptional<ProcessDataItemT>(element.Descendants(IODDDeviceFunctionNames.ProcessDataOutName).FirstOrDefault());

        return new ProcessDataT(condition, pdin, pdout);
    }
}