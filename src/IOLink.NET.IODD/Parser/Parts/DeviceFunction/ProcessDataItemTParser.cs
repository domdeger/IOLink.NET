using System.Xml.Linq;
using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Parts.Constants;
using IOLink.NET.IODD.Parts.Datatypes;
using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.ProcessData;

namespace IOLink.NET.IODD.Parts.DeviceFunction;

internal class ProcessDataItemParser : IParserPart<ProcessDataItemT>
{
    private static readonly IEnumerable<XName> Names = new[]
    {
        IODDDeviceFunctionNames.ProcessDataInName,
        IODDDeviceFunctionNames.ProcessDataOutName,
    };
    private readonly IParserPartLocator _parserLocator;

    public ProcessDataItemParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }

    public bool CanParse(XName name) => Names.Contains(name);

    public ProcessDataItemT Parse(XElement element)
    {
        DatatypeT? datatypeT = DatatypeTParser.ParseOptional(
            element.Descendants(IODDParserConstants.DatatypeName).FirstOrDefault(),
            _parserLocator
        );
        DatatypeRefT? datatypeRef = _parserLocator.ParseOptional<DatatypeRefT>(
            element.Descendants(IODDParserConstants.DatatypeRefName).FirstOrDefault()
        );
        var id = element.ReadMandatoryAttributeAsString("id");
        ushort bitLength = element.ReadMandatoryAttribute<ushort>("bitLength");

        return new ProcessDataItemT(datatypeT, datatypeRef, id, bitLength);
    }
}
