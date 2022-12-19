using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser;

internal interface IParserPartLocator
{
    T Parse<T>(XElement element);
}