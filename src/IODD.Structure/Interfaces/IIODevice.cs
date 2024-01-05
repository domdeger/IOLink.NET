using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Interfaces.ExternalTextCollection;
using IOLinkNET.IODD.Structure.Interfaces.Profile;
using IOLinkNET.IODD.Structure.Profile;
using IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLinkNET.IODD.Structure.Interfaces;
public interface IIODevice
{
    IProfileBodyT ProfileBody { get; }
    IExternalTextCollectionT ExternalTextCollection { get; }
    IEnumerable<DatatypeT> StandardDatatypeCollection { get; }
}
