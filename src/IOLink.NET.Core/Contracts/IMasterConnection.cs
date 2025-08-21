namespace IOLink.NET.Core.Contracts;

public interface IMasterConnection
{
    Task<byte> GetPortCountAsync(CancellationToken cancellationToken);

    Task<IPortInformation> GetPortInformationAsync(
        byte portNumber,
        CancellationToken cancellationToken
    );

    Task<IPortInformation[]> GetPortInformationsAsync(CancellationToken cancellationToken);

    Task<ReadOnlyMemory<byte>> ReadIndexAsync(
        byte portNumber,
        ushort index,
        CancellationToken cancellationToken,
        byte subIindex = 0
    );

    Task<ReadOnlyMemory<byte>> ReadProcessDataInAsync(
        byte portNumber,
        CancellationToken cancellationToken
    );

    Task<ReadOnlyMemory<byte>> ReadProcessDataOutAsync(
        byte portNumber,
        CancellationToken cancellationToken
    );
}
