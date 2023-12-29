using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Interfaces;

namespace IOLinkNET.IODD.Resolution;

internal class DatatypeResolver
{
    private readonly List<DatatypeT> _datatypes = new();

    public DatatypeResolver(IEnumerable<DatatypeT> datatypes, IEnumerable<DatatypeT> standardDatatypes)
    {
        _datatypes.AddRange(standardDatatypes);
        _datatypes.AddRange(datatypes);
    }

    public DatatypeT Resolve(IDatatypeOrTypeRef resolvee)
        => resolvee.Type ?? _datatypes.FirstOrDefault(type => type.Id == resolvee.Ref?.DatatypeId)
                ?? throw new ArgumentOutOfRangeException(nameof(resolvee), "Datatype could not be resolved.");
}