using System.Xml.Linq;

namespace IOLinkNET.IODD.Parts.Constants;

public static class IODDDeviceFunctionNames
{
    public static readonly XName DatatypeCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("DatatypeCollection");

    public static readonly XName DatatypeName = IODDParserConstants.IODDXmlNamespace.GetName("Datatype");

    public static readonly XName VariableCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("VariableCollection");

    public static readonly XName VariableName = IODDParserConstants.IODDXmlNamespace.GetName("Variable");

    public static readonly XName RecordItemName = IODDParserConstants.IODDXmlNamespace.GetName("RecordItem");

    public static readonly XName RecordItemInfoName = IODDParserConstants.IODDXmlNamespace.GetName("RecordItemInfo");

    public static readonly XName ConditionName = IODDParserConstants.IODDXmlNamespace.GetName("Condition");

    public static readonly XName ProcessDataName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessData");

    public static readonly XName ProcessDataInName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessDataIn");

    public static readonly XName ProcessDataOutName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessDataOut");

    public static readonly XName ProcessDataCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("ProcessDataCollection");

    public static readonly XName UserInterfaceName = IODDParserConstants.IODDXmlNamespace.GetName("UserInterface");

    public static readonly XName MenuCollectionName = IODDParserConstants.IODDXmlNamespace.GetName("MenuCollection");

    public static readonly XName IdentificationMenuName = IODDParserConstants.IODDXmlNamespace.GetName("IdentificationMenu");

    public static readonly XName ParameterMenuName = IODDParserConstants.IODDXmlNamespace.GetName("ParameterMenu");

    public static readonly XName ObservationMenuName = IODDParserConstants.IODDXmlNamespace.GetName("ObservationMenu");

    public static readonly XName DiagnosisMenuName = IODDParserConstants.IODDXmlNamespace.GetName("DiagnosisMenu");

    public static readonly XName ObserverRoleMenuSetName = IODDParserConstants.IODDXmlNamespace.GetName("ObserverRoleMenuSet");

    public static readonly XName MaintenanceRoleMenuSetName = IODDParserConstants.IODDXmlNamespace.GetName("MaintenanceRoleMenuSet");

    public static readonly XName SpecialistRoleMenuSetName = IODDParserConstants.IODDXmlNamespace.GetName("SpecialistRoleMenuSet");

    public static readonly XName MenuName = IODDParserConstants.IODDXmlNamespace.GetName("Menu");

    public static readonly XName MenuItemName = IODDParserConstants.IODDXmlNamespace.GetName("Name");

    public static readonly XName VariableRefName = IODDParserConstants.IODDXmlNamespace.GetName("VariableRef");

    public static readonly XName MenuRefName = IODDParserConstants.IODDXmlNamespace.GetName("MenuRef");

    public static readonly XName RecordItemRefName = IODDParserConstants.IODDXmlNamespace.GetName("RecordItemRef");
}