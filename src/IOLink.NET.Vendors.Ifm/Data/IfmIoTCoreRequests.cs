namespace IOLink.NET.Vendors.Ifm.Data;

public record IfmIoTCoreServiceRequestBase(string Adr, string Code = "request", int Cid = 1337);

public record IfmIoTCoreServiceParameterizedRequest<T>(string Adr, T? Data, string Code = "request", int Cid = 1337) : IfmIoTCoreServiceRequestBase(Adr, Code, Cid);

record GetTreeParameters(string? Adr, int? Level);

record IfmIoTGetTreeRequest(GetTreeParameters? Data) : IfmIoTCoreServiceParameterizedRequest<GetTreeParameters>("GetTree", Data);

record IfmIoTGetIdentityRequest() : IfmIoTCoreServiceRequestBase("GetIdentity");

public record IfmIoTReadAcyclicRequest(int port, int index, int? subindex) : IfmIoTCoreServiceParameterizedRequest<IfmIoTAcyclicParameters>($"iolinkmaster/port[{port}]/iolinkdevice/iolreadacyclic", new(index, subindex));

public record IfmIoTReadPdInRequest(int port) : IfmIoTCoreServiceRequestBase($"iolinkmaster/port[{port}]/iolinkdevice/pdin/getdata");

public record IfmIoTReadPdOutRequest(int port) : IfmIoTCoreServiceRequestBase($"iolinkmaster/port[{port}]/iolinkdevice/pdout/getdata");

public record IfmIoTGetDataMultiRequest(IEnumerable<string> Paths) : IfmIoTCoreServiceParameterizedRequest<IfmIoTGetDataMultiParameters>("GetDataMulti", new(Paths));

public record IfmIoTGetDataMultiParameters(IEnumerable<string> Datatosend);

public record IfmIoTGetPortTreeRequest() : IfmIoTCoreServiceParameterizedRequest<IfmIoTGetTreeParameters>("gettree", new("iolinkmaster/", 1));

public record IfmIoTGetTreeParameters(string? Adr, int? Level);

public record IfmIoTAcyclicParameters(int index, int? subindex);
