using System.Xml.Linq;

using IOLink.NET.IODD.Parts.Constants;
using IOLink.NET.IODD.Parts.Datatypes;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.IODD.Parts;

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
