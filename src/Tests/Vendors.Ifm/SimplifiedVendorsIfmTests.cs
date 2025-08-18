namespace IOLink.NET.Vendors.Ifm.Tests;

public class SimplifiedVendorsIfmTests
{
    [Fact]
    public void IfmIoTCoreClientFactory_Create_WithValidUrl_ReturnsClient()
    {
        // Arrange
        var baseUrl = "https://test.example.com";

        // Act
        var client = IfmIoTCoreClientFactory.Create(baseUrl);

        // Assert
        client.ShouldNotBeNull();
        client.ShouldBeAssignableTo<IIfmIoTCoreClient>();
    }

    [Fact]
    public void IfmIoTCoreMasterConnectionFactory_Create_WithValidUrl_ReturnsConnection()
    {
        // Arrange
        var baseUrl = "https://test.example.com";

        // Act
        var connection = IfmIoTCoreMasterConnectionFactory.Create(baseUrl);

        // Assert
        connection.ShouldNotBeNull();
        connection.ShouldBeOfType<IfmIotCoreMasterConnection>();
        connection.ShouldBeAssignableTo<IMasterConnection>();
    }

    [Fact]
    public void IfmIotCoreMasterConnection_Constructor_WithValidClient_CreatesInstance()
    {
        // Arrange
        var mockClient = Substitute.For<IIfmIoTCoreClient>();

        // Act
        var connection = new IfmIotCoreMasterConnection(mockClient);

        // Assert
        connection.ShouldNotBeNull();
        connection.ShouldBeAssignableTo<IMasterConnection>();
    }

    [Fact]
    public void IfmIotCoreMasterConnection_Constructor_WithValidClient_DoesNotThrow()
    {
        // Arrange
        var client = Substitute.For<IIfmIoTCoreClient>();

        // Act & Assert
        Should.NotThrow(() => new IfmIotCoreMasterConnection(client));
    }

    [Fact]
    public void IIfmIoTCoreClient_IsPublicInterface()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);

        // Act & Assert
        interfaceType.IsInterface.ShouldBeTrue();
        interfaceType.IsPublic.ShouldBeTrue();
    }

    [Fact]
    public void IIfmIoTCoreClient_HasExpectedMethods()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var expectedMethods = new[]
        {
            "GetMasterDeviceTagAsync",
            "GetDeviceAcyclicDataAsync",
            "GetDevicePdinDataAsync",
            "GetDevicePdoutDataAsync",
            "GetDataMultiAsync",
            "GetPortTreeAsync",
        };

        // Act & Assert
        foreach (var methodName in expectedMethods)
        {
            var method = interfaceType.GetMethod(methodName);
            method.ShouldNotBeNull($"Method {methodName} should exist");
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void FactoryMethods_WithInvalidUrl_ThrowException(string invalidUrl)
    {
        // Act & Assert - URI constructor throws UriFormatException for invalid URIs
        Should.Throw<UriFormatException>(() => IfmIoTCoreClientFactory.Create(invalidUrl));
        Should.Throw<UriFormatException>(
            () => IfmIoTCoreMasterConnectionFactory.Create(invalidUrl)
        );
    }
}
