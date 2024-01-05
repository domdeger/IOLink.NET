using System.Xml.Linq;

using IOLinkNET.IODD.Standard.Constants;

namespace IOLinkNET.IODD.Parts.Constants;

public static class IODDDeviceFunctionNames
{
    public static readonly XName DatatypeCollectionName = IODDConstants.IODDXmlNamespace.GetName("DatatypeCollection");

    public static readonly XName DatatypeName = IODDConstants.IODDXmlNamespace.GetName("Datatype");

    public static readonly XName VariableCollectionName = IODDConstants.IODDXmlNamespace.GetName("VariableCollection");

    public static readonly XName VariableName = IODDConstants.IODDXmlNamespace.GetName("Variable");

    public static readonly XName StandardVariableRefName = IODDConstants.IODDXmlNamespace.GetName("StdVariableRef");

    public static readonly XName RecordItemName = IODDConstants.IODDXmlNamespace.GetName("RecordItem");

    public static readonly XName RecordItemInfoName = IODDConstants.IODDXmlNamespace.GetName("RecordItemInfo");

    public static readonly XName ConditionName = IODDConstants.IODDXmlNamespace.GetName("Condition");

    public static readonly XName ProcessDataName = IODDConstants.IODDXmlNamespace.GetName("ProcessData");

    public static readonly XName ProcessDataInName = IODDConstants.IODDXmlNamespace.GetName("ProcessDataIn");

    public static readonly XName ProcessDataOutName = IODDConstants.IODDXmlNamespace.GetName("ProcessDataOut");

    public static readonly XName ProcessDataCollectionName = IODDConstants.IODDXmlNamespace.GetName("ProcessDataCollection");

    public static readonly XName UserInterfaceName = IODDConstants.IODDXmlNamespace.GetName("UserInterface");

    public static readonly XName MenuCollectionName = IODDConstants.IODDXmlNamespace.GetName("MenuCollection");

    public static readonly XName IdentificationMenuName = IODDConstants.IODDXmlNamespace.GetName("IdentificationMenu");

    public static readonly XName ParameterMenuName = IODDConstants.IODDXmlNamespace.GetName("ParameterMenu");

    public static readonly XName ObservationMenuName = IODDConstants.IODDXmlNamespace.GetName("ObservationMenu");

    public static readonly XName DiagnosisMenuName = IODDConstants.IODDXmlNamespace.GetName("DiagnosisMenu");

    public static readonly XName ObserverRoleMenuSetName = IODDConstants.IODDXmlNamespace.GetName("ObserverRoleMenuSet");

    public static readonly XName MaintenanceRoleMenuSetName = IODDConstants.IODDXmlNamespace.GetName("MaintenanceRoleMenuSet");

    public static readonly XName SpecialistRoleMenuSetName = IODDConstants.IODDXmlNamespace.GetName("SpecialistRoleMenuSet");

    public static readonly XName MenuName = IODDConstants.IODDXmlNamespace.GetName("Menu");

    public static readonly XName MenuItemName = IODDConstants.IODDXmlNamespace.GetName("Name");

    public static readonly XName VariableRefName = IODDConstants.IODDXmlNamespace.GetName("VariableRef");

    public static readonly XName MenuRefName = IODDConstants.IODDXmlNamespace.GetName("MenuRef");

    public static readonly XName RecordItemRefName = IODDConstants.IODDXmlNamespace.GetName("RecordItemRef");
}