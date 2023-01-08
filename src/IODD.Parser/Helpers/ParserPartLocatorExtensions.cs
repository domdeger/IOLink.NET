using System.Xml.Linq;

using IOLinkNET.IODD.Parser;

namespace IOLinkNET.IODD.Helpers
{
    internal static class ParserPartLocatorExtensions
    {
        public static T? ParseOptional<T>(this IParserPartLocator locator, XElement? element)
            where T : class
        {
            return element is null ? null : locator.Parse<T>(element);
        }

        public static T ParseMandatory<T>(this IParserPartLocator locator, XElement? element)
        {
            return element is null
                ? throw new ArgumentNullException(nameof(element))
                : locator.Parse<T>(element) ?? throw new InvalidOperationException("Could not parse the element as expected.");
        }
    }
}