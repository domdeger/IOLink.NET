using IOLink.NET.IODD.Structure.Interfaces.ExternalTextCollection;
using IOLink.NET.IODD.Structure.Structure.Datatypes;

namespace IOLink.NET.IODD.Structure.Structure.ExternalTextCollection;
public record ExternalTextCollectionT(PrimaryLanguageT PrimaryLanguage, IEnumerable<TextDefinitionT> TextDefinitions): IExternalTextCollectionT; 
