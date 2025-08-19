using System.Text.Json.Nodes;

namespace IOLink.NET.Vendors.Ifm.Data;

public record IfmIoTCoreResponseBase<T>(T Data, int Cid, int Code);

public record IfmIoTCoreValueWrapper<T>(T Value);

public record IfmIoTCoreScalarResponse<T>(IfmIoTCoreValueWrapper<T> Data, int Cid, int Code)
    : IfmIoTCoreResponseBase<IfmIoTCoreValueWrapper<T>>(Data, Cid, Code);

public record IfmIoTCoreComplexResponse<T>(T Data, int Cid, int Code)
    : IfmIoTCoreResponseBase<T>(Data, Cid, Code);

public record IfmIoTCoreGetDataMultiEntry(int Code, JsonValue Data);

public record IfmIoTCorePortTreeResponse(IfmIoTCoreTreeStructure Data, int Cid, int Code)
    : IfmIoTCoreComplexResponse<IfmIoTCoreTreeStructure>(Data, Cid, Code);

public record IfmIoTCoreTreeStructure(
    IEnumerable<IfmIoTCoreTreeStructure>? Subs,
    string Identifier
);
