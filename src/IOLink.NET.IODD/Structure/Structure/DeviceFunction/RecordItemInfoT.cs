namespace IOLink.NET.IODD.Structure.DeviceFunction;

public record RecordItemInfoT(byte SubIndex, object? DefaultValue, bool ModifiesOtherVariables = false, bool ExcludedFromDataStorage = false);
