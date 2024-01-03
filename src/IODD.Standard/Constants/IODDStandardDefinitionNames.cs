using System.Xml.Linq;

namespace IOLinkNET.IODD.Standard.Constants;
public static class IODDStandardDefinitionNames
{
    public static readonly XName VariableName = IODDConstants.IODDXmlNamespace.GetName("Variable");

    public static readonly XName VariableCollectionName = IODDConstants.IODDXmlNamespace.GetName("VariableCollection");

    public static readonly XName DatatypeCollectionName = IODDConstants.IODDXmlNamespace.GetName("DatatypeCollection");
}
