namespace IOLink.NET.Core.Tests.Models;

public class PortInformationTests
{
    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        byte expectedPortNumber = 5;
        PortStatus expectedStatus = PortStatus.Connected | PortStatus.IOLink;
        var deviceInfo = new DeviceInformation(123, 456, "test-device");

        // Act
        var portInfo = new PortInformation(expectedPortNumber, expectedStatus, deviceInfo);

        // Assert
        portInfo.PortNumber.ShouldBe(expectedPortNumber);
        portInfo.Status.ShouldBe(expectedStatus);
        portInfo.DeviceInformation.ShouldBe(deviceInfo);
    }

    [Fact]
    public void Constructor_WithNullDeviceInformation_SetsDeviceInformationToNull()
    {
        // Arrange
        byte portNumber = 3;
        PortStatus status = PortStatus.Disconnected;

        // Act
        var portInfo = new PortInformation(portNumber, status, null);

        // Assert
        portInfo.PortNumber.ShouldBe(portNumber);
        portInfo.Status.ShouldBe(status);
        portInfo.DeviceInformation.ShouldBeNull();
    }

    [Fact]
    public void PortNumber_IsReadOnly()
    {
        // Arrange
        var portInfo = new PortInformation(1, PortStatus.Connected, null);

        // Act & Assert
        portInfo.PortNumber.ShouldBe((byte)1);
        // Property should not have a setter (read-only)
        typeof(PortInformation)
            .GetProperty(nameof(PortInformation.PortNumber))!
            .CanWrite.ShouldBeFalse();
    }

    [Fact]
    public void Status_IsReadOnly()
    {
        // Arrange
        var expectedStatus = PortStatus.Error;
        var portInfo = new PortInformation(1, expectedStatus, null);

        // Act & Assert
        portInfo.Status.ShouldBe(expectedStatus);
        // Property should not have a setter (read-only)
        typeof(PortInformation)
            .GetProperty(nameof(PortInformation.Status))!
            .CanWrite.ShouldBeFalse();
    }

    [Fact]
    public void DeviceInformation_IsReadOnly()
    {
        // Arrange
        var deviceInfo = new DeviceInformation(123, 456, "test");
        var portInfo = new PortInformation(1, PortStatus.Connected, deviceInfo);

        // Act & Assert
        portInfo.DeviceInformation.ShouldBe(deviceInfo);
        // Property should not have a setter (read-only)
        typeof(PortInformation)
            .GetProperty(nameof(PortInformation.DeviceInformation))!
            .CanWrite.ShouldBeFalse();
    }

    [Theory]
    [InlineData((byte)0, PortStatus.Disconnected)]
    [InlineData((byte)1, PortStatus.Connected)]
    [InlineData((byte)8, PortStatus.IOLink)]
    [InlineData((byte)16, PortStatus.Error)]
    [InlineData((byte)255, PortStatus.DI)]
    [InlineData((byte)128, PortStatus.Connected | PortStatus.IOLink | PortStatus.Error)]
    public void Constructor_HandlesVariousPortNumbersAndStatuses(byte portNumber, PortStatus status)
    {
        // Arrange
        var deviceInfo = new DeviceInformation(100, 200, "test-device");

        // Act
        var portInfo = new PortInformation(portNumber, status, deviceInfo);

        // Assert
        portInfo.PortNumber.ShouldBe(portNumber);
        portInfo.Status.ShouldBe(status);
        portInfo.DeviceInformation.ShouldBe(deviceInfo);
    }

    [Fact]
    public void ImplementsIPortInformation()
    {
        // Arrange & Act
        var portInfo = new PortInformation(1, PortStatus.Connected, null);

        // Assert
        portInfo.ShouldBeAssignableTo<IPortInformation>();
    }

    [Fact]
    public void Constructor_WithAllPortStatusFlags_WorksCorrectly()
    {
        // Arrange
        var allFlags = PortStatus.Connected | PortStatus.IOLink | PortStatus.Error | PortStatus.DI;
        var deviceInfo = new DeviceInformation(999, 888, "complex-device");

        // Act
        var portInfo = new PortInformation(7, allFlags, deviceInfo);

        // Assert
        portInfo.Status.ShouldBe(allFlags);
        portInfo.Status.HasFlag(PortStatus.Connected).ShouldBeTrue();
        portInfo.Status.HasFlag(PortStatus.IOLink).ShouldBeTrue();
        portInfo.Status.HasFlag(PortStatus.Error).ShouldBeTrue();
        portInfo.Status.HasFlag(PortStatus.DI).ShouldBeTrue();
    }
}
