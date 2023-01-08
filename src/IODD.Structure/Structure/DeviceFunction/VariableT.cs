using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Structure.DeviceFunction;

public record VariableT(string Id, ushort Index, DatatypeT? Datatype, DatatypeRefT? DatatypeRef, TextRefT Name, TextRefT? Description,
    AccessRightsT AccessRights, IEnumerable<RecordItemInfoT> RecordItemInfos,
    bool Dynamic = false, bool ModifiesOtherVariables = false, bool ExcludedFromDataStorage = false)
    : AbstractVariableT(Datatype, DatatypeRef, Name, Description, AccessRights, RecordItemInfos, Dynamic, ModifiesOtherVariables, ExcludedFromDataStorage);