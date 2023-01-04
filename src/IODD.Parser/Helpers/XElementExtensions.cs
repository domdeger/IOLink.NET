using System.Xml.Linq;

namespace IOLinkNET.IODD.Helpers;

internal static class XElementExtensions
{
    public static T ReadMandatoryAttribute<T>(this XElement element, string attributeName)
        where T : IParsable<T>
    {
        string value = element.ReadMandatoryAttribute(attributeName);
        return T.Parse(value, null);
    }

    public static string ReadMandatoryAttribute(this XElement element, string attributeName, XNamespace? xmlNamespace = null)
    {
        XName fqName = xmlNamespace is not null ? xmlNamespace.GetName(attributeName) : attributeName;

        XAttribute attribute = element.Attribute(fqName) ?? throw new ArgumentOutOfRangeException($"{attributeName} does not exist on this element");
        return attribute.Value;
    }

    public static string? ReadOptionalAttribute(this XElement element, string attributeName)
    {
        XAttribute? attribute = element.Attribute(element.Name + attributeName);
        return attribute?.Value;
    }

    public static T? ReadOptionalAttribute<T>(this XElement element, string attributeName)
            where T : IParsable<T>
    {
        string? value = element.ReadOptionalAttribute(attributeName);
        return value is not null ? T.Parse(value, null) : default;
    }
}