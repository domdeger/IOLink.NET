using System.Xml.Linq;

using IOLinkNET.IODD.Standard.Constants;

namespace IOLinkNET.IODD.Standard.Structure;
public static class StandardDefinitionReader
{
    private static readonly XElement _ioddStandardDefinitions;

    static StandardDefinitionReader()
    {
        using (var reader = new StreamReader("./XML/IODD-StandardDefinitions1.1.xml"))
        {
            _ioddStandardDefinitions = XElement.Load(reader);
        }
    }

    public static IEnumerable<XElement>? GetVariableCollection()
    {
        return _ioddStandardDefinitions?.Elements(IODDConstants.IODDXmlNamespace.GetName("VariableCollection"));
    }

    public static XElement? GetDatatypeCollection()
    {
        return _ioddStandardDefinitions?.Elements(IODDConstants.IODDXmlNamespace.GetName("DatatypeCollection")).FirstOrDefault();
    }
}
