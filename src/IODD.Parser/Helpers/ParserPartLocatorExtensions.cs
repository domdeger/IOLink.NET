using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser;

internal static class ParserPartLocatorExtensions
{
    public static T? ParseOptional<T>(this IParserPartLocator locator, XElement? element)
        where T : class
    {
        if (element is null)
        {
            return null;
        }

        return locator.Parse<T>(element);
    }

    public static T ParseMandatory<T>(this IParserPartLocator locator, XElement? element)
    {
        if (element is null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        return locator.Parse<T>(element) ?? throw new InvalidOperationException("Could not parse the element as expected.");
    }
}