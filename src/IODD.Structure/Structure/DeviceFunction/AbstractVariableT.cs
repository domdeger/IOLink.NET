using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Interfaces;

namespace IOLinkNET.IODD.Structure.DeviceFunction;

public record AbstractVariableT(DatatypeT? Type, DatatypeRefT? Ref, TextRefT Name, TextRefT? Description,
    AccessRightsT AccessRights, IEnumerable<RecordItemInfoT> RecordItemInfos,
    bool Dynamic = false, bool ModifiesOtherVariables = false, bool ExcludedFromDataStorage = false) : IDatatypeOrTypeRef;