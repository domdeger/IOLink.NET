using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Parser.Parts.Constants;
using IOLinkNET.IODD.Parts.Constants;
using IOLinkNET.IODD.Parts.Datatypes;
using IOLinkNET.IODD.Standard.Structure;
using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;

namespace IOLinkNET.IODD.Parts.DeviceFunction;

internal class StandardVariableRefTParser
{
    private readonly IParserPartLocator _parserLocator;

    public StandardVariableRefTParser(IParserPartLocator parserLocator)
    {
        _parserLocator = parserLocator;
    }
    public bool CanParse(XName name)
        => name == IODDDeviceFunctionNames.StandardVariableRefName;

    public VariableT Parse(XElement element)
    {
        var variableId = element.ReadMandatoryAttribute("id");
        var stdVariableCollection = StandardDefinitionReader.GetVariableCollection();
        var stdVariable = stdVariableCollection.Elements(IODDStandardDefinitionNames.VariableName).Where(x => x.ReadMandatoryAttribute("id") == variableId).Single();

        DatatypeT? dataType = DatatypeTParser.ParseOptional(stdVariable.Descendants(IODDParserConstants.DatatypeName).FirstOrDefault(), _parserLocator);
        DatatypeRefT? dataTypeRef = _parserLocator.ParseOptional<DatatypeRefT>(stdVariable.Descendants(IODDParserConstants.DatatypeRefName).FirstOrDefault());
        TextRefT name = _parserLocator.ParseMandatory<TextRefT>(stdVariable.Element(IODDTextRefNames.Name));
        TextRefT? description = _parserLocator.ParseOptional<TextRefT>(stdVariable.Element(IODDTextRefNames.DescriptionName));
        AccessRightsT accessRights = AccessRightsTConverter.Parse(stdVariable.ReadMandatoryAttribute("accessRights"));
        IEnumerable<RecordItemInfoT> recordItemInfos = stdVariable.Descendants(IODDDeviceFunctionNames.RecordItemInfoName).Select(_parserLocator.Parse<RecordItemInfoT>);
        ushort index = stdVariable.ReadMandatoryAttribute<ushort>("index");
        var id = stdVariable.ReadMandatoryAttribute("id");
        return new VariableT(id, index, dataType, dataTypeRef, name, description, accessRights, recordItemInfos);
    }
}