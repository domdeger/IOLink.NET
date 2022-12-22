using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser.Parts;

internal class ParserPartLocator : IParserPartLocator
{
    private IList<IParserPart> _parserParts = new List<IParserPart>();

    public ParserPartLocator(IEnumerable<IParserPart> parts)
    {
        AddParts(parts);
    }

    public ParserPartLocator()
    {

    }

    public void AddPart(IParserPart part)
    {
        _parserParts.Add(part);
    }

    private void AddParts(IEnumerable<IParserPart> parts)
    {
        foreach (var part in parts)
        {
            AddPart(part);
        }
    }

    public T Parse<T>(XElement element)
    {
        var part = _parserParts.OfType<IParserPart<T>>().FirstOrDefault(part => part.CanParse(element.Name))
            ?? throw new InvalidOperationException("Could not find suitable implementation part.");

        return part.Parse(element);
    }
}