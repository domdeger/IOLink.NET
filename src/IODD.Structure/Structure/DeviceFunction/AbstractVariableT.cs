using IOLinkNET.IODD.Structure.DataTypes;
using IOLinkNET.IODD.Structure.Common;
namespace IOLinkNET.IODD.Structure.DeviceFunction;

public record AbstractVariableT(DatatypeT? Datatype, DatatypeRefT? DatatypeRef, TextRefT Name, TextRefT? Description,
    AccessRightsT AccessRights, IEnumerable<RecordItemInfoT> RecordItemInfos,
    bool Dynamic = false, bool ModifiesOtherVariables = false, bool ExcludedFromDataStorage = false);