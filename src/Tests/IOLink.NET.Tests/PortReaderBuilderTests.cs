using IOLink.NET.Core.Contracts;
using IOLink.NET.Integration;
using IOLink.NET.IODD.Provider;
using IOLink.NET.IODD.Resolution.Contracts;
using NSubstitute;

namespace IOLink.NET.Tests;

public class PortReaderBuilderTests
{
    [Fact]
    public void ShouldThrowExceptionWhenNecessaryParametersMissing()
    {
        var builderAction = () => PortReaderBuilder.NewPortReader().Build();

        Should.Throw<InvalidOperationException>(builderAction);
    }

    [Fact]
    public void ShouldThrowExceptionWhenMasterConnectionIsMissing()
    {
        var builderAction = () =>
            PortReaderBuilder
                .NewPortReader()
                .WithDeviceDefinitionProvider(Substitute.For<IDeviceDefinitionProvider>())
                .Build();

        Should.Throw<InvalidOperationException>(builderAction);
    }

    [Fact]
    public void ShouldThrowExceptionWhenDeviceDefinitionProviderIsMissing()
    {
        var builderAction = () =>
            PortReaderBuilder
                .NewPortReader()
                .WithMasterConnection(Substitute.For<IMasterConnection>())
                .Build();

        Should.Throw<InvalidOperationException>(builderAction);
    }

    [Fact]
    public void ShouldThrowExceptionWhenIoddDataConverterIsMissing()
    {
        var builderAction = () =>
            PortReaderBuilder
                .NewPortReader()
                .WithMasterConnection(Substitute.For<IMasterConnection>())
                .WithDeviceDefinitionProvider(Substitute.For<IDeviceDefinitionProvider>())
                .Build();

        Should.Throw<InvalidOperationException>(builderAction);
    }

    [Fact]
    public void ShouldThrowExceptionWhenTypeResolverFactoryIsMissing()
    {
        var builderAction = () =>
            PortReaderBuilder
                .NewPortReader()
                .WithMasterConnection(Substitute.For<IMasterConnection>())
                .WithDeviceDefinitionProvider(Substitute.For<IDeviceDefinitionProvider>())
                .WithIoddDataConverter(Substitute.For<IIoddDataConverter>())
                .Build();

        Should.Throw<InvalidOperationException>(builderAction);
    }

    [Fact]
    public void ShouldBuildPortReader()
    {
        var builderAction = () =>
            PortReaderBuilder
                .NewPortReader()
                .WithMasterConnection(Substitute.For<IMasterConnection>())
                .WithDeviceDefinitionProvider(Substitute.For<IDeviceDefinitionProvider>())
                .WithIoddDataConverter(Substitute.For<IIoddDataConverter>())
                .WithTypeResolverFactory(Substitute.For<ITypeResolverFactory>())
                .Build();

        Should.NotThrow(builderAction);
    }
}
