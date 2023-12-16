namespace IOLinkNET.Device.Contract;

public interface IMasterConnection
{
    Task<byte> GetPortCountAsync(CancellationToken cancellationToken = default);

    Task<IPortInformation> GetPortInformationAsync(byte portNumber, CancellationToken cancellationToken = default);

    Task<IPortInformation[]> GetPortInformationsAsync(CancellationToken cancellationToken = default);

    Task<ReadOnlyMemory<byte>> ReadIndexAsync(byte portNumber, ushort index, byte subIindex = 0, CancellationToken cancellationToken = default);

    Task<ReadOnlyMemory<byte>> ReadProcessDataInAsync(byte portNumber, CancellationToken cancellationToken = default);

    Task<ReadOnlyMemory<byte>> ReadProcessDataOutAsync(byte portNumber, CancellationToken cancellationToken = default);
}