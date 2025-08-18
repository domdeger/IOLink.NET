using IOLink.NET.IODD.Structure.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLink.NET.IODD.Structure.Interfaces.ExternalTextCollection;
public interface IExternalTextCollectionT
{
    PrimaryLanguageT PrimaryLanguage { get; }
    IEnumerable<TextDefinitionT> TextDefinitions { get; }
}
