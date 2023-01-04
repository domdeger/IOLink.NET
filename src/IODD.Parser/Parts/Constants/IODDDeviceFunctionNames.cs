using System.Xml.Linq;

namespace IOLinkNET.IODD.Parts.Constants;

public static class IODDDeviceFunctionNames
{
    public static readonly XName DatatypeCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("DatatypeCollection");

    public static readonly XName DatatypeName = IODDParserConstants.IODDXmlNamespace.GetName("Datatype");

    public static readonly XName VariableCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("VariableCollection");

    public static readonly XName VariableName = IODDParserConstants.IODDXmlNamespace.GetName("Variable");

    public static readonly XName RecordItemInfo = IODDParserConstants.IODDXmlNamespace.GetName("RecordItemInfo");

    public static readonly XName ConditionName = IODDParserConstants.IODDXmlNamespace.GetName("Condition");

    public static readonly XName ProcessDataName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessData");

    public static readonly XName ProcessDataInName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessDataIn");

    public static readonly XName ProcessDataOutName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessDataOut");

    public static readonly XName ProcessDataCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessDataCollection");
}