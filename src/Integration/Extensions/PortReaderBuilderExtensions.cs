using IOLinkNET.Conversion;
using IOLinkNET.IODD.Provider;
using IOLinkNET.IODD.Resolution.Common;

namespace IOLinkNET.Integration;

public static class PortReaderBuilderExtensions
{
    public static PortReaderBuilder WithDefaultTypeResolverFactory(this PortReaderBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.WithTypeResolverFactory(new DefaultTypeResolverFactory());
        return builder;
    }

    public static PortReaderBuilder WithPublicIODDFinderApi(this PortReaderBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        var ioddFinderApiClient = new IODDFinderPublicClient();
        var ioddProvider = new DeviceDefinitionProvider(ioddFinderApiClient);

        builder.WithDeviceDefinitionProvider(ioddProvider);

        return builder;
    }

    public static PortReaderBuilder WithDefaultIoddConverter(this PortReaderBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.WithIoddDataConverter(new IoddConverter());

        return builder;
    }
}