using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Interfaces;
using IOLink.NET.IODD.Structure.Interfaces.ExternalTextCollection;
using IOLink.NET.IODD.Structure.Interfaces.Profile;

namespace IOLink.NET.IODD.Structure;

public record IODevice(IProfileBodyT ProfileBody, IExternalTextCollectionT ExternalTextCollection, IEnumerable<DatatypeT> StandardDatatypeCollection): IIODevice;
