using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser.Parts;

internal class ParserPartLocator : IParserPartLocator
{
    IEnumerable<IParserPart> ParserParts { get; }

    public ParserPartLocator()
    {
        ParserParts = new List<IParserPart>();
    }

    public T Parse<T>(XElement element)
    {
        var part = ParserParts.OfType<IParserPart<T>>().FirstOrDefault(part => part.Target == element.Name)
            ?? throw new InvalidOperationException("Could not find suitable implementation part.");

        return part.Parse(element);
    }
}