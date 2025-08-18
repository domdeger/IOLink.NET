using System.Xml.Linq;

using IOLink.NET.IODD.Parser;

namespace IOLink.NET.IODD.Parts;

internal class ParserPartLocator : IParserPartLocator
{
    private readonly IList<IParserPart> _parserParts = new List<IParserPart>();

    public ParserPartLocator(IEnumerable<IParserPart> parts)
    {
        AddParts(parts);
    }

    public ParserPartLocator()
    {

    }

    public void AddPart(IParserPart part) => _parserParts.Add(part);

    private void AddParts(IEnumerable<IParserPart> parts)
    {
        foreach (IParserPart part in parts)
        {
            AddPart(part);
        }
    }

    public T Parse<T>(XElement element)
    {
        IParserPart<T> part = _parserParts.OfType<IParserPart<T>>().FirstOrDefault(part => part.CanParse(element.Name))
            ?? throw new InvalidOperationException($"Could not find suitable implementation part for {typeof(T).Name}.");

        return part.Parse(element);
    }
}
