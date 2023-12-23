using IOLinkNET.IODD.Structure.Profile;
using IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;

namespace IOLinkNET.IODD.Structure;

public record IODevice(ProfileBodyT ProfileBody, ExternalTextCollectionT ExternalTextCollection);