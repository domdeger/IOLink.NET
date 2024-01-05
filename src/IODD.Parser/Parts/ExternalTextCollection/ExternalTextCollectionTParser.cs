using System.Xml.Linq;

using IOLinkNET.IODD.Parser.Parts.Constants;
using IOLinkNET.IODD.Structure.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLinkNET.IODD.Parser.Parts.ExternalTextCollection;
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
