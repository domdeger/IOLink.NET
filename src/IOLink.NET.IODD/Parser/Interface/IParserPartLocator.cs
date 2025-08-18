using System.Xml.Linq;

namespace IOLink.NET.IODD.Parser;

internal interface IParserPartLocator
{
    T Parse<T>(XElement element);
    void AddPart(IParserPart part);
}
