using System.Xml.Linq;

using IODD.Parser.Helpers;
using IODD.Parser.Parts.Constants;
using IODD.Parser.Parts.Datatypes;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IODD.Parser.Parts.DeviceFunction;

internal class ProcessDataItemParser : IParserPart<ProcessDataItemT>
{
    private static readonly IEnumerable<XName> Names = new[] { IODDDeviceFunctionNames.ProcessDataInName, IODDDeviceFunctionNames.ProcessDataOutName };
    private readonly IParserPartLocator _parserLocator;

    public ProcessDataItemParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }

    public bool CanParse(XName name)
        => Names.Contains(name);

    public ProcessDataItemT Parse(XElement element)
    {
        DatatypeT? datatypeT = DatatypeTParser.ParseOptional(element.Descendants(IODDParserConstants.DatatypeName).FirstOrDefault(), _parserLocator);
        DatatypeRefT? datatypeRef = _parserLocator.ParseOptional<DatatypeRefT>(element.Descendants(IODDParserConstants.DatatypeRefName).FirstOrDefault());
        TextRefT? name = _parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.Name).FirstOrDefault());

        ushort bitLength = element.ReadMandatoryAttribute<ushort>("bitLength");

        return new ProcessDataItemT(datatypeT, datatypeRef, name, bitLength);
    }
}