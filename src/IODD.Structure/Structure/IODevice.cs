using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Interfaces;
using IOLinkNET.IODD.Structure.Interfaces.ExternalTextCollection;
using IOLinkNET.IODD.Structure.Interfaces.Profile;

namespace IOLinkNET.IODD.Structure;

public record IODevice(IProfileBodyT ProfileBody, IExternalTextCollectionT ExternalTextCollection, IEnumerable<DatatypeT> StandardDatatypeCollection): IIODevice;