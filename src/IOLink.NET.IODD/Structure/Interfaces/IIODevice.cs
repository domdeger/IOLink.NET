using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Interfaces.ExternalTextCollection;
using IOLink.NET.IODD.Structure.Interfaces.Profile;
using IOLink.NET.IODD.Structure.Profile;
using IOLink.NET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLink.NET.IODD.Structure.Interfaces;
public interface IIODevice
{
    IProfileBodyT ProfileBody { get; }
    IExternalTextCollectionT ExternalTextCollection { get; }
    IEnumerable<DatatypeT> StandardDatatypeCollection { get; }
}
