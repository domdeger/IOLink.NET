using IOLinkNET.Vendors.Ifm.Data;

using Refit;

namespace IOLinkNET.Vendors.Ifm;

public interface IIfmIoTCoreClient
{
    [Get("/devicetag/applicationtag/getdata")]
    Task<IfmIoTCoreScalarResponse<string>> GetMasterDeviceTagAsync(CancellationToken cancellationToken);

    [Post("")]
    Task<IfmIoTCoreScalarResponse<string>> GetDeviceAcyclicData(IfmIoTReadAcyclicRequest request, CancellationToken cancellationToken);

    [Post("")]
    Task<IfmIoTCoreScalarResponse<string>> GetDevicePdinData(IfmIoTReadPdInRequest request, CancellationToken cancellationToken);

    [Post("")]
    Task<IfmIoTCoreScalarResponse<string>> GetDevicePdoutData(IfmIoTReadPdOutRequest request, CancellationToken cancellationToken);

}
