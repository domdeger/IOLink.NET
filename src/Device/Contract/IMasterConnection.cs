namespace IOLinkNET.Device.Contract;

public interface IMasterConnection
{
    public Task<byte> GetPortCountAsync(CancellationToken cancellationToken = default);


}