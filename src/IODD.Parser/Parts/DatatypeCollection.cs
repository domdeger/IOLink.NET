using System.Xml.Linq;

using IODD.Parser.Helpers;
using IODD.Parser.Parts.Constants;
using IODD.Parser.Parts.Datatypes;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.DataTypes;

namespace IODD.Parser.Parts;

internal class DatatypeCollection : IParserPart<IEnumerable<DatatypeT>>
{
    private readonly IParserPartLocator _partLocator;

    public DatatypeCollection(IParserPartLocator partLocator)
    {
        _partLocator = partLocator;
    }

    public bool CanParse(XName name)
        => name == IODDParserConstants.DeviceFunctionName;

    public IEnumerable<DatatypeT> Parse(XElement element)
    {
        IEnumerable<XElement> dataTypeElements = element.Descendants(IODDDeviceFunctionNames.DatatypeName);
        List<DatatypeT> dataTypeCollection = new();

        foreach (XElement dataTypeElement in dataTypeElements)
        {
            string typeName = dataTypeElement.ReadMandatoryAttribute("type");

            DatatypeT convertedType = (DatatypeT?)SimpleTypeParser.Parse(typeName, dataTypeElement) ?? ComplexTypeParser.Parse(typeName, dataTypeElement, _partLocator)
                    ?? throw new NotSupportedException($"Could not parse data type with name {typeName}.");

            dataTypeCollection.Add(convertedType);
        }

        return dataTypeCollection;
    }
}