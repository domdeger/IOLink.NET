using System.Xml.Linq;

namespace IODD.Parser.Parts.Constants;

public static class IODDDeviceFunctionNames
{
    public static readonly XName DatatypeCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("DatatypeCollection");

    public static readonly XName DatatypeName = IODDParserConstants.IODDXmlNamespace.GetName("Datatype");

    public static readonly XName VariableCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("VariableCollection");

    public static readonly XName VariableName = IODDParserConstants.IODDXmlNamespace.GetName("Variable");

    public static readonly XName RecordItemInfo = IODDParserConstants.IODDXmlNamespace.GetName("RecordItemInfo");

    public static readonly XName ProcessDataCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessDataCollection");
}