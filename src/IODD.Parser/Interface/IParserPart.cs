using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser;

internal interface IParserPart<T> : IParserPart
{
    T Parse(XElement element);
}

internal interface IParserPart
{
    bool CanParse(XName target);
}