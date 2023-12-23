using IOLinkNET.IODD.Structure.Structure.Datatypes;

namespace IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;
public record ExternalTextCollectionT(PrimaryLanguageT PrimaryLanguage, IEnumerable<TextDefinitionT> TextDefinitions); 
