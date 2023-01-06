using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;
using IOLinkNET.IODD.Parts.Datatypes;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;

namespace IOLinkNET.IODD.Parts.DeviceFunction;

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
        IEnumerable<RecordItemInfoT> recordItemInfos = element.Descendants(IODDDeviceFunctionNames.RecordItemInfoName).Select(_parserLocator.Parse<RecordItemInfoT>);
        ushort index = element.ReadMandatoryAttribute<ushort>("index");
        
        return new VariableT(index, dataType, dataTypeRef, name, description, accessRights, recordItemInfos);
    }
}