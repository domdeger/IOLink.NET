namespace IOLinkNET.Vendors.Ifm.Data;

public record IfmIoTCoreServiceRequestBase(string Adr, string Code = "request", int Cid = 1337);

public record IfmIoTCoreServiceParameterizedRequest<T>(string Adr, T? Data, string Code = "request", int Cid = 1337) : IfmIoTCoreServiceRequestBase(Adr, Code, Cid);

record GetTreeParameters(string? Adr, int? Level);

record IfmIoTGetTreeRequest(GetTreeParameters? Data) : IfmIoTCoreServiceParameterizedRequest<GetTreeParameters>("GetTree", Data);

record IfmIoTGetIdentityRequest() : IfmIoTCoreServiceRequestBase("GetIdentity");

public record IfmIoTReadAcyclicRequest(int port, int index, int? subindex) : IfmIoTCoreServiceParameterizedRequest<IfmIoTAcyclicParameters>($"iolinkmaster/port[{port}]/iolinkdevice/iolreadacyclic", new(index, subindex));

public record IfmIoTReadPdInRequest(int port) : IfmIoTCoreServiceRequestBase($"iolinkmaster/port[{port}]/iolinkdevice/pdin/getdata");

public record IfmIoTReadPdOutRequest(int port) : IfmIoTCoreServiceRequestBase($"iolinkmaster/port[{port}]/iolinkdevice/pdout/getdata");

public record IfmIoTAcyclicParameters(int index, int? subindex);