using IOLinkNET.Vendors.Ifm.Data;

using Refit;

namespace IOLinkNET.Vendors.Ifm;

public interface IIfmIoTCoreClient
{
    [Get("/devicetag/applicationtag/getdata")]
    Task<IfmIoTCoreScalarResponse<string>> GetMasterDeviceTagAsync(CancellationToken cancellationToken);

    [Post("")]
    Task<IfmIoTCoreScalarResponse<string>> GetDeviceAcyclicDataAsync(IfmIoTReadAcyclicRequest request, CancellationToken cancellationToken);

    [Post("")]
    Task<IfmIoTCoreScalarResponse<string>> GetDevicePdinDataAsync(IfmIoTReadPdInRequest request, CancellationToken cancellationToken);

    [Post("")]
    Task<IfmIoTCoreScalarResponse<string>> GetDevicePdoutDataAsync(IfmIoTReadPdOutRequest request, CancellationToken cancellationToken);

    [Post("")]
    Task<IfmIoTCoreComplexResponse<Dictionary<string, IfmIoTCoreGetDataMultiEntry>>> GetDataMultiAsync(IfmIoTGetDataMultiRequest request, CancellationToken cancellationToken);

}
