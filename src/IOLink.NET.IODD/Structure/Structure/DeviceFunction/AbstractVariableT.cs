using IOLink.NET.IODD.Structure.Common;
using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.Interfaces;

namespace IOLink.NET.IODD.Structure.DeviceFunction;

public record AbstractVariableT(DatatypeT? Type, DatatypeRefT? Ref, TextRefT Name, TextRefT? Description,
    AccessRightsT AccessRights, IEnumerable<RecordItemInfoT> RecordItemInfos,
    bool Dynamic = false, bool ModifiesOtherVariables = false, bool ExcludedFromDataStorage = false) : IDatatypeOrTypeRef;
