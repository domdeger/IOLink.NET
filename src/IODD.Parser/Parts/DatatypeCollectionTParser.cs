using System.Xml.Linq;

using IOLinkNET.IODD.Parts.Constants;
using IOLinkNET.IODD.Parts.Datatypes;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts;

internal class DatatypeCollectionTParser : IParserPart<IEnumerable<DatatypeT>>
{
    private readonly IParserPartLocator _partLocator;

    public DatatypeCollectionTParser(IParserPartLocator partLocator)
    {
        _partLocator = partLocator;
    }

    public bool CanParse(XName name)
        => name == IODDParserConstants.DatatypeCollectionName;

    public IEnumerable<DatatypeT> Parse(XElement element)
    {
        IEnumerable<XElement> dataTypeElements = element.Descendants(IODDDeviceFunctionNames.DatatypeName);
        List<DatatypeT> dataTypeCollection = new();

        foreach (XElement dataTypeElement in dataTypeElements)
        {
            DatatypeT convertedType = DatatypeTParser.Parse(dataTypeElement, _partLocator);
            dataTypeCollection.Add(convertedType);
        }

        return dataTypeCollection;
    }
}