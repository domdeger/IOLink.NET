using System.Xml.Linq;

namespace IOLinkNET.IODD.Parser;

internal static class XElementExtensions
{
    public static T ReadMandatoryAttribute<T>(this XElement element, string attributeName)
        where T : IParsable<T>
    {
        var value = ReadMandatoryAttribute(element, attributeName);
        return T.Parse(value, null);
    }

    public static string ReadMandatoryAttribute(this XElement element, string attributeName)

    {
        var attribute = element.Attribute(element.Name + attributeName) ?? throw new ArgumentOutOfRangeException($"{attributeName} does not exist on this element");
        return attribute.Value;
    }

    public static string? ReadOptionalAttribute(this XElement element, string attributeName)
    {
        var attribute = element.Attribute(element.Name + attributeName);
        return attribute?.Value;
    }

    public static T? ReadOptionalAttribute<T>(this XElement element, string attributeName)
            where T : class, IParsable<T>
    {
        var value = ReadOptionalAttribute(element, attributeName);
        if (value is not null)
        {
            return T.Parse(value, null);
        }

        return null;
    }
}