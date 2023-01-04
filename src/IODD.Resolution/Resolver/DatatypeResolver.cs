using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Interfaces;

namespace IODD.Resolution;

internal class DatatypeResolver
{
    private readonly IEnumerable<DatatypeT> _datatypes;

    public DatatypeResolver(IEnumerable<DatatypeT> datatypes)
    {
        _datatypes = datatypes;
    }

    public DatatypeT Resolve(IDatatypeOrTypeRef resolvee)
        => resolvee.Type ?? _datatypes.FirstOrDefault(type => type.Id == resolvee.Ref?.DatatypeId) 
                ?? throw new ArgumentOutOfRangeException(nameof(resolvee), "Datatype could not be resolved.");
}