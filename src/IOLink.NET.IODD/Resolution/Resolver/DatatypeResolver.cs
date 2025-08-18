using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Interfaces;

namespace IOLink.NET.IODD.Resolution;

internal class DatatypeResolver
{
    private readonly DatatypeT[] _datatypes;

    public DatatypeResolver(IEnumerable<DatatypeT> datatypes, IEnumerable<DatatypeT> standardDatatypes)
    {
        _datatypes = datatypes.Concat(standardDatatypes).ToArray();
    }

    public DatatypeT Resolve(IDatatypeOrTypeRef resolvee)
        => resolvee.Type ?? _datatypes.FirstOrDefault(type => type.Id == resolvee.Ref?.DatatypeId)
                ?? throw new ArgumentOutOfRangeException(nameof(resolvee), "Datatype could not be resolved.");
}
