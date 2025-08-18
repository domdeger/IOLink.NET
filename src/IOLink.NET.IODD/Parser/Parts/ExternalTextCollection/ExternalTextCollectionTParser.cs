using System.Xml.Linq;

using IOLink.NET.IODD.Parser.Parts.Constants;
using IOLink.NET.IODD.Structure.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLink.NET.IODD.Parser.Parts.ExternalTextCollection;
internal class ExternalTextCollectionTParser : IParserPart<ExternalTextCollectionT>
{
    public bool CanParse(XName name) => name == IODDParserConstants.ExternalTextCollectionName;
    public ExternalTextCollectionT Parse(XElement element)
    {
        XElement? primaryLanguageElement = element.Elements(IODDExternalCollectionNames.PrimaryLanguageName).First();
        PrimaryLanguageT parsedPrimaryLanguage = PrimaryLanguageTParser.Parse(primaryLanguageElement);

        IEnumerable<XElement> textDefinitionElements = primaryLanguageElement.Elements(IODDExternalCollectionNames.TextName);
        List<TextDefinitionT> textDefinitions = new();

        foreach (XElement textDefinitionElement in textDefinitionElements)
        {
            TextDefinitionT parsedTextDefinition = TextDefinitionTParser.Parse(textDefinitionElement);
            textDefinitions.Add(parsedTextDefinition);
        }

        return new ExternalTextCollectionT(parsedPrimaryLanguage, textDefinitions);
    }
}
