using System.Xml.Linq;

using IODD.Parser.Helpers;
using IODD.Parser.Parts.Constants;
using IODD.Parser.Parts.Datatypes;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;

namespace IODD.Parser.Parts.DeviceFunction;

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
        TextRefT name = _parserLocator.ParseMandatory<TextRefT>(element.Descendants(IODDTextRefNames.Name).FirstOrDefault());
        TextRefT? description = _parserLocator.ParseOptional<TextRefT>(element.Descendants(IODDTextRefNames.DescriptionName).FirstOrDefault());
        AccessRightsT accessRights = AccessRightsTConverter.Parse(element.ReadMandatoryAttribute("accessRights"));
        IEnumerable<RecordItemInfoT> recordItemInfos = element.Descendants(IODDDeviceFunctionNames.RecordItemInfo).Select(_parserLocator.Parse<RecordItemInfoT>);

        return new VariableT(dataType, dataTypeRef, name, description, accessRights, recordItemInfos);
    }
}