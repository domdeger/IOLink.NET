namespace IOLink.NET.Core.Tests.Models;

public class DeviceInformationTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        ushort expectedVendorId = 123;
        uint expectedDeviceId = 456789;
        string expectedProductId = "TEST-PRODUCT-123";

        // Act
        var deviceInfo = new DeviceInformation(
            expectedVendorId,
            expectedDeviceId,
            expectedProductId
        );

        // Assert
        deviceInfo.VendorId.ShouldBe(expectedVendorId);
        deviceInfo.DeviceId.ShouldBe(expectedDeviceId);
        deviceInfo.ProductId.ShouldBe(expectedProductId);
    }

    [Fact]
    public void VendorId_IsReadOnly()
    {
        // Arrange
        var deviceInfo = new DeviceInformation(123, 456, "test");

        // Act & Assert
        deviceInfo.VendorId.ShouldBe((ushort)123);
        // Property should not have a setter (read-only)
        typeof(DeviceInformation)
            .GetProperty(nameof(DeviceInformation.VendorId))!
            .CanWrite.ShouldBeFalse();
    }

    [Fact]
    public void DeviceId_IsReadOnly()
    {
        // Arrange
        var deviceInfo = new DeviceInformation(123, 456, "test");

        // Act & Assert
        deviceInfo.DeviceId.ShouldBe((uint)456);
        // Property should not have a setter (read-only)
        typeof(DeviceInformation)
            .GetProperty(nameof(DeviceInformation.DeviceId))!
            .CanWrite.ShouldBeFalse();
    }

    [Fact]
    public void ProductId_IsReadOnly()
    {
        // Arrange
        string expectedProductId = "test-product";
        var deviceInfo = new DeviceInformation(123, 456, expectedProductId);

        // Act & Assert
        deviceInfo.ProductId.ShouldBe(expectedProductId);
        // Property should not have a setter (read-only)
        typeof(DeviceInformation)
            .GetProperty(nameof(DeviceInformation.ProductId))!
            .CanWrite.ShouldBeFalse();
    }

    [Theory]
    [InlineData((ushort)0, (uint)0, "")]
    [InlineData((ushort)1, (uint)1, "A")]
    [InlineData(
        (ushort)65535,
        (uint)4294967295,
        "VERY-LONG-PRODUCT-ID-WITH-SPECIAL-CHARS-!@#$%^&*()"
    )]
    public void Constructor_HandlesEdgeCases(ushort vendorId, uint deviceId, string productId)
    {
        // Act
        var deviceInfo = new DeviceInformation(vendorId, deviceId, productId);

        // Assert
        deviceInfo.VendorId.ShouldBe(vendorId);
        deviceInfo.DeviceId.ShouldBe(deviceId);
        deviceInfo.ProductId.ShouldBe(productId);
    }

    [Fact]
    public void Constructor_WithNullProductId_AllowsNull()
    {
        // Arrange & Act - The actual implementation accepts null values
        var deviceInfo = new DeviceInformation(123, 456, null!);

        // Assert
        deviceInfo.VendorId.ShouldBe((ushort)123);
        deviceInfo.DeviceId.ShouldBe((uint)456);
        deviceInfo.ProductId.ShouldBeNull();
    }

    [Fact]
    public void ImplementsIDeviceInformation()
    {
        // Arrange & Act
        var deviceInfo = new DeviceInformation(123, 456, "test");

        // Assert
        deviceInfo.ShouldBeAssignableTo<IDeviceInformation>();
    }
}
