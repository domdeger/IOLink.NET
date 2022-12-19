using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser;

internal interface IParserPart<T> : IParserPart
{
    T Parse(XElement element);
}

internal interface IParserPart
{
    XName Target { get; }
}