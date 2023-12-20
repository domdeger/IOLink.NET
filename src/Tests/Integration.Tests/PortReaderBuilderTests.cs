using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.Device.Contract;
using IOLinkNET.Integration;
using IOLinkNET.IODD.Provider;
using IOLinkNET.IODD.Resolution.Contracts;

using NSubstitute;

namespace Integration.Tests;

public class PortReaderBuilderTests
{
    [Fact]
    public void ShouldThrowExceptionWhenNecessaryParametersMissing()
    {
        var builderAction = () => PortReaderBuilder.NewPortReader().Build();

        builderAction.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenMasterConnectionIsMissing()
    {
        var builderAction = () => PortReaderBuilder.NewPortReader()
            .WithDeviceDefinitionProvider(Substitute.For<IDeviceDefinitionProvider>())
            .Build();

        builderAction.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenDeviceDefinitionProviderIsMissing()
    {
        var builderAction = () => PortReaderBuilder.NewPortReader()
            .WithMasterConnection(Substitute.For<IMasterConnection>())
            .Build();

        builderAction.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenIoddDataConverterIsMissing()
    {
        var builderAction = () => PortReaderBuilder.NewPortReader()
            .WithMasterConnection(Substitute.For<IMasterConnection>())
            .WithDeviceDefinitionProvider(Substitute.For<IDeviceDefinitionProvider>())
            .Build();

        builderAction.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ShouldThrowExceptionWhenTypeResolverFactoryIsMissing()
    {
        var builderAction = () => PortReaderBuilder.NewPortReader()
            .WithMasterConnection(Substitute.For<IMasterConnection>())
            .WithDeviceDefinitionProvider(Substitute.For<IDeviceDefinitionProvider>())
            .WithIoddDataConverter(Substitute.For<IIoddDataConverter>())
            .Build();

        builderAction.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void ShouldBuildPortReader()
    {
        var builderAction = () => PortReaderBuilder.NewPortReader()
            .WithMasterConnection(Substitute.For<IMasterConnection>())
            .WithDeviceDefinitionProvider(Substitute.For<IDeviceDefinitionProvider>())
            .WithIoddDataConverter(Substitute.For<IIoddDataConverter>())
            .WithTypeResolverFactory(Substitute.For<ITypeResolverFactory>())
            .Build();

        builderAction.Should().NotThrow();
    }
}