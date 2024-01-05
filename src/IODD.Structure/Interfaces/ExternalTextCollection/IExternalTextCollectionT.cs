using IOLinkNET.IODD.Structure.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLinkNET.IODD.Structure.Interfaces.ExternalTextCollection;
public interface IExternalTextCollectionT
{
    PrimaryLanguageT PrimaryLanguage { get; }
    IEnumerable<TextDefinitionT> TextDefinitions { get; }
}
