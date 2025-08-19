using System.IO.Compression;
using System.Xml.Linq;
using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure;

namespace IOLink.NET.IODD.Provider;

public class DeviceDefinitionProvider : IDeviceDefinitionProvider
{
    private readonly IIODDProvider _ioddProvider;
    private readonly IODDParser _ioddParser = new IODDParser();

    public DeviceDefinitionProvider(IIODDProvider ioddProvider)
    {
        _ioddProvider = ioddProvider;
    }

    public async Task<IODevice> GetDeviceDefinitionAsync(
        ushort vendorId,
        uint deviceId,
        string productId,
        CancellationToken cancellationToken
    )
    {
        var ioddPackage = await _ioddProvider
            .GetIODDPackageAsync(vendorId, deviceId, productId, cancellationToken)
            .ConfigureAwait(false);

        using var zipArchive = new ZipArchive(ioddPackage, ZipArchiveMode.Read);
        var ioddXml = await FindMainIoddEntryAsync(zipArchive, cancellationToken)
            .ConfigureAwait(false);

        return _ioddParser.Parse(
            ioddXml.Root ?? throw new InvalidOperationException("No root element found")
        );
    }

    private async Task<XDocument> FindMainIoddEntryAsync(
        ZipArchive zipArchive,
        CancellationToken cancellationToken
    )
    {
        var xmlFiles = zipArchive.Entries.Where(e =>
            e.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)
        );

        foreach (var xmlFile in xmlFiles)
        {
            using var xmlFileStream = xmlFile.Open();
            var xml = await XDocument
                .LoadAsync(xmlFileStream, LoadOptions.None, cancellationToken)
                .ConfigureAwait(false);
            if (IODDParser.IsIODDFile(xml))
            {
                return xml;
            }
        }

        throw new InvalidOperationException("No matching IODD file found");
    }
}
