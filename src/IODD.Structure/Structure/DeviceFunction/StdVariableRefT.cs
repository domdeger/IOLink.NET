namespace IOLinkNET.IODD.Structure.DeviceFunction;

public record StdVariableRefT(string Id, byte SubIndex, byte? FixedLengthRestriction, bool ExcludedFromDataStorage = false);