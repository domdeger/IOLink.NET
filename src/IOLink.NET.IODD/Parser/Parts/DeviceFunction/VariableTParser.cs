using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parts.Constants;
using IOLink.NET.IODD.Parts.Datatypes;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.DeviceFunction;

namespace IOLink.NET.IODD.Parts.DeviceFunction;

internal class VariableTParser : IParserPart<VariableT>
{
    private readonly IParserPartLocator _parserLocator;

    public VariableTParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }
    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.VariableName;

    public VariableT Parse(XElement element)
    {
        DatatypeT? dataType = DatatypeTParser.ParseOptional(element.Descendants(IODDParserConstants.DatatypeName).FirstOrDefault(), _parserLocator);
        DatatypeRefT? dataTypeRef = _parserLocator.ParseOptional<DatatypeRefT>(element.Descendants(IODDParserConstants.DatatypeRefName).FirstOrDefault());
        TextRefT name = _parserLocator.ParseMandatory<TextRefT>(element.Element(IODDTextRefNames.Name));
        TextRefT? description = _parserLocator.ParseOptional<TextRefT>(element.Element(IODDTextRefNames.DescriptionName));
        AccessRightsT accessRights = AccessRightsTConverter.Parse(element.ReadMandatoryAttribute("accessRights"));
        IEnumerable<RecordItemInfoT> recordItemInfos = element.Descendants(IODDDeviceFunctionNames.RecordItemInfoName).Select(_parserLocator.Parse<RecordItemInfoT>);
        ushort index = element.ReadMandatoryAttribute<ushort>("index");
        var id = element.ReadMandatoryAttribute("id");

        return new VariableT(id, index, dataType, dataTypeRef, name, description, accessRights, recordItemInfos);
    }
}
