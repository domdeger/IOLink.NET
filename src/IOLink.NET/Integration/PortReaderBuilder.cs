using IOLink.NET.Conversion;
using IOLink.NET.Core.Contracts;
using IOLink.NET.IODD.Provider;
using IOLink.NET.IODD.Resolution.Contracts;

namespace IOLink.NET.Integration;

public class PortReaderBuilder
{
    private IMasterConnection? _masterConnection;
    private IDeviceDefinitionProvider? _deviceDefinitionProvider;
    private IIoddDataConverter? _ioddDataConverter;
    private ITypeResolverFactory? _typeResolverFactory;

    public static PortReaderBuilder NewPortReader()
    {
        return new PortReaderBuilder();
    }

    public PortReaderBuilder WithMasterConnection(IMasterConnection masterConnection)
    {
        if (masterConnection is null)
        {
            throw new ArgumentNullException(nameof(masterConnection));
        }

        if (_masterConnection is not null)
        {
            throw new InvalidOperationException("MasterConnection is already set");
        }

        _masterConnection = masterConnection;
        return this;
    }

    public PortReaderBuilder WithDeviceDefinitionProvider(
        IDeviceDefinitionProvider deviceDefinitionProvider
    )
    {
        if (deviceDefinitionProvider is null)
        {
            throw new ArgumentNullException(nameof(deviceDefinitionProvider));
        }

        if (_deviceDefinitionProvider is not null)
        {
            throw new InvalidOperationException("DeviceDefinitionProvider is already set");
        }

        _deviceDefinitionProvider = deviceDefinitionProvider;
        return this;
    }

    public PortReaderBuilder WithIoddDataConverter(IIoddDataConverter ioddDataConverter)
    {
        if (ioddDataConverter is null)
        {
            throw new ArgumentNullException(nameof(ioddDataConverter));
        }

        if (_ioddDataConverter is not null)
        {
            throw new InvalidOperationException("IoddDataConverter is already set");
        }

        _ioddDataConverter = ioddDataConverter;
        return this;
    }

    public PortReaderBuilder WithTypeResolverFactory(ITypeResolverFactory typeResolverFactory)
    {
        if (typeResolverFactory is null)
        {
            throw new ArgumentNullException(nameof(typeResolverFactory));
        }

        if (_typeResolverFactory is not null)
        {
            throw new InvalidOperationException("TypeResolverFactory is already set");
        }

        _typeResolverFactory = typeResolverFactory;
        return this;
    }

    public IODDPortReader Build()
    {
        if (_masterConnection is null)
        {
            throw new InvalidOperationException("MasterConnection is not set");
        }

        if (_deviceDefinitionProvider is null)
        {
            throw new InvalidOperationException("DeviceDefinitionProvider is not set");
        }

        if (_ioddDataConverter is null)
        {
            throw new InvalidOperationException("IoddDataConverter is not set");
        }

        if (_typeResolverFactory is null)
        {
            throw new InvalidOperationException("TypeResolverFactory is not set");
        }

        return new IODDPortReader(
            _masterConnection,
            _deviceDefinitionProvider,
            _ioddDataConverter,
            _typeResolverFactory
        );
    }
}
