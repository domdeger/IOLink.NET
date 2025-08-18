using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;
using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Parser.Parts.Constants;
using IOLink.NET.IODD.Parts.Constants;
using IOLink.NET.IODD.Parts.Datatypes;
using IOLink.NET.IODD.Standard.Structure;
using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.DeviceFunction;

namespace IOLink.NET.IODD.Parts.DeviceFunction;

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

        var fixedLengthRestriction = element.ReadOptionalAttribute<byte>("fixedLengthRestriction");
        DatatypeT? dataType = DatatypeTParser.ParseOptional(stdVariable.Descendants(IODDParserConstants.DatatypeName).FirstOrDefault(), fixedLengthRestriction, _parserLocator);
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
