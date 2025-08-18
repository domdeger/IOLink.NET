namespace IOLink.NET.Core.Tests.Contracts;

public class PortStatusTests
{
    [Fact]
    public void PortStatus_HasCorrectValues()
    {
        // Assert
        ((byte)PortStatus.Disconnected).ShouldBe((byte)0);
        ((byte)PortStatus.Connected).ShouldBe((byte)1);
        ((byte)PortStatus.IOLink).ShouldBe((byte)2);
        ((byte)PortStatus.Error).ShouldBe((byte)4);
        ((byte)PortStatus.DI).ShouldBe((byte)8);
    }

    [Fact]
    public void PortStatus_CanBeCombinedWithFlags()
    {
        // Arrange & Act
        var combinedStatus = PortStatus.Connected | PortStatus.IOLink;

        // Assert
        combinedStatus.HasFlag(PortStatus.Connected).ShouldBeTrue();
        combinedStatus.HasFlag(PortStatus.IOLink).ShouldBeTrue();
        combinedStatus.HasFlag(PortStatus.Error).ShouldBeFalse();
        combinedStatus.HasFlag(PortStatus.DI).ShouldBeFalse();
        // Note: Disconnected (0) will always return true for HasFlag, so we don't test it here
    }

    [Fact]
    public void PortStatus_AllFlagsCombination()
    {
        // Arrange & Act
        var allFlags = PortStatus.Connected | PortStatus.IOLink | PortStatus.Error | PortStatus.DI;

        // Assert
        allFlags.HasFlag(PortStatus.Connected).ShouldBeTrue();
        allFlags.HasFlag(PortStatus.IOLink).ShouldBeTrue();
        allFlags.HasFlag(PortStatus.Error).ShouldBeTrue();
        allFlags.HasFlag(PortStatus.DI).ShouldBeTrue();
        ((byte)allFlags).ShouldBe((byte)15); // 1 + 2 + 4 + 8 = 15
    }

    [Theory]
    [InlineData(PortStatus.Disconnected, 0)]
    [InlineData(PortStatus.Connected, 1)]
    [InlineData(PortStatus.IOLink, 2)]
    [InlineData(PortStatus.Error, 4)]
    [InlineData(PortStatus.DI, 8)]
    public void PortStatus_IndividualValuesAreCorrect(PortStatus status, byte expectedValue)
    {
        // Assert
        ((byte)status).ShouldBe(expectedValue);
    }

    [Fact]
    public void PortStatus_IsMarkedWithFlagsAttribute()
    {
        // Arrange
        var portStatusType = typeof(PortStatus);

        // Act
        var hasFlagsAttribute = portStatusType
            .GetCustomAttributes(typeof(FlagsAttribute), false)
            .Any();

        // Assert
        hasFlagsAttribute.ShouldBeTrue();
    }

    [Fact]
    public void PortStatus_IsOfTypeByte()
    {
        // Assert
        typeof(PortStatus).GetEnumUnderlyingType().ShouldBe(typeof(byte));
    }

    [Theory]
    [InlineData(PortStatus.Connected | PortStatus.IOLink, true, true, false, false)]
    [InlineData(PortStatus.Error | PortStatus.DI, false, false, true, true)]
    [InlineData(PortStatus.Connected | PortStatus.Error, true, false, true, false)]
    public void PortStatus_ComplexFlagCombinations(
        PortStatus status,
        bool shouldBeConnected,
        bool shouldBeIOLink,
        bool shouldBeError,
        bool shouldBeDI
    )
    {
        // Assert
        status.HasFlag(PortStatus.Connected).ShouldBe(shouldBeConnected);
        status.HasFlag(PortStatus.IOLink).ShouldBe(shouldBeIOLink);
        status.HasFlag(PortStatus.Error).ShouldBe(shouldBeError);
        status.HasFlag(PortStatus.DI).ShouldBe(shouldBeDI);
    }

    [Fact]
    public void PortStatus_DisconnectedDoesNotHaveAnyFlags()
    {
        // Arrange
        var disconnectedStatus = PortStatus.Disconnected;

        // Assert
        disconnectedStatus.HasFlag(PortStatus.Connected).ShouldBeFalse();
        disconnectedStatus.HasFlag(PortStatus.IOLink).ShouldBeFalse();
        disconnectedStatus.HasFlag(PortStatus.Error).ShouldBeFalse();
        disconnectedStatus.HasFlag(PortStatus.DI).ShouldBeFalse();
    }
}
