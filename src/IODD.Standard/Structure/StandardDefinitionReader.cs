using System.Xml.Linq;
using IOLinkNET.IODD.Standard.Constants;

namespace IOLinkNET.IODD.Standard.Structure;

public static class StandardDefinitionReader
{
    private static readonly XElement _ioddStandardDefinitions;

    static StandardDefinitionReader()
    {
        var standardDefinitionPath = "./XML/IODD-StandardDefinitions1.1.xml";

        if (!File.Exists(standardDefinitionPath))
        {
            throw new FileNotFoundException(
                $"IODD-StandardDefinitions1.1.xml must be present to use {nameof(StandardDefinitionReader)}"
            );
        }

        using (var reader = new StreamReader(standardDefinitionPath))
        {
            _ioddStandardDefinitions = XElement.Load(reader);
        }
    }

    public static XElement GetVariableCollection()
    {
        var variableCollection = _ioddStandardDefinitions
            .Elements(IODDStandardDefinitionNames.VariableCollectionName)
            .Single();
        return variableCollection
            ?? throw new InvalidOperationException(
                "VariableCollection cannot be null in standard definitions"
            );
    }

    public static XElement GetDatatypeCollection()
    {
        var dataTypeCollection = _ioddStandardDefinitions
            .Elements(IODDStandardDefinitionNames.DatatypeCollectionName)
            .Single();
        return dataTypeCollection
            ?? throw new InvalidOperationException(
                "DatatypeCollection cannot be null in standard definitions"
            );
    }
}
