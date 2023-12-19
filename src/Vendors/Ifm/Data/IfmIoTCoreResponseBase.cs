namespace IOLinkNET.Vendors.Ifm.Data;

public record IfmIoTCoreResponseBase<T>(T Data, int Cid, int Code);

public record IfmIoTCoreValueWrapper<T>(T Value);

public record IfmIoTCoreScalarResponse<T>(IfmIoTCoreValueWrapper<T> Data, int Cid, int Code) : IfmIoTCoreResponseBase<IfmIoTCoreValueWrapper<T>>(Data, Cid, Code);