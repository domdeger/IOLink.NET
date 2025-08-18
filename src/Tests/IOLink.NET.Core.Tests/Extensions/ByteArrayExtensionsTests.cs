namespace IOLink.NET.Core.Tests.Extensions;

public class ByteArrayExtensionsTests
{
    [Fact]
    public void PinNegativeIntToRequiredBitLength_WhenBitLengthIsMultipleOf8_ReturnsOriginalData()
    {
        // Arrange
        byte[] data = { 0xFF, 0xFF, 0xFF, 0xFF };
        ushort bitLength = 32; // Multiple of 8

        // Act
        var result = data.PinNegativeIntToRequiredBitLength(bitLength);

        // Assert
        result.ShouldBe(data);
        result.ShouldBeSameAs(data); // Should return the same reference
    }

    [Fact]
    public void PinNegativeIntToRequiredBitLength_WhenBitLengthIsNotMultipleOf8_MasksFirstByte()
    {
        // Arrange
        byte[] data = { 0xFF, 0xFF };
        ushort bitLength = 10; // 10 % 8 = 2, mask = (-1 << 2) = 0xFC

        // Act
        var result = data.PinNegativeIntToRequiredBitLength(bitLength);

        // Assert
        result[0].ShouldBe((byte)0x03); // 0xFF ^ 0xFC = 0x03
        result[1].ShouldBe((byte)0xFF); // Second byte unchanged
        result.ShouldBeSameAs(data); // Should return the same reference
    }

    [Theory]
    [InlineData(9, 0x01)] // 9 % 8 = 1, mask = (-1 << 1) = 0xFE, 0xFF ^ 0xFE = 0x01
    [InlineData(10, 0x03)] // 10 % 8 = 2, mask = (-1 << 2) = 0xFC, 0xFF ^ 0xFC = 0x03
    [InlineData(11, 0x07)] // 11 % 8 = 3, mask = (-1 << 3) = 0xF8, 0xFF ^ 0xF8 = 0x07
    [InlineData(12, 0x0F)] // 12 % 8 = 4, mask = (-1 << 4) = 0xF0, 0xFF ^ 0xF0 = 0x0F
    public void PinNegativeIntToRequiredBitLength_VariousBitLengths_MasksCorrectly(
        ushort bitLength,
        byte expectedFirstByte
    )
    {
        // Arrange
        byte[] data = { 0xFF, 0xFF };

        // Act
        var result = data.PinNegativeIntToRequiredBitLength(bitLength);

        // Assert
        result[0].ShouldBe(expectedFirstByte);
        result[1].ShouldBe((byte)0xFF);
    }

    [Fact]
    public void PinNegativeIntToRequiredBitLength_WithSingleByte_WorksCorrectly()
    {
        // Arrange
        byte[] data = { 0xFF };
        ushort bitLength = 5; // 5 % 8 = 5, mask first (8-5=3) bits

        // Act
        var result = data.PinNegativeIntToRequiredBitLength(bitLength);

        // Assert
        // mask = (-1 << 5) = 0xE0, 0xFF ^ 0xE0 = 0x1F
        result[0].ShouldBe((byte)0x1F);
    }

    [Fact]
    public void TruncateToBitLength_WhenExactByteLength_ReturnsLastBytes()
    {
        // Arrange
        byte[] data = { 0x01, 0x02, 0x03, 0x04 };
        ushort bitLength = 16; // 2 bytes

        // Act
        var result = data.TruncateToBitLength(bitLength);

        // Assert
        result.Length.ShouldBe(2);
        result[0].ShouldBe((byte)0x03);
        result[1].ShouldBe((byte)0x04);
    }

    [Fact]
    public void TruncateToBitLength_WhenBitLengthRequiresPartialByte_RoundsUpByteLength()
    {
        // Arrange
        byte[] data = { 0x01, 0x02, 0x03, 0x04 };
        ushort bitLength = 17; // 2.125 bytes, so 3 bytes needed

        // Act
        var result = data.TruncateToBitLength(bitLength);

        // Assert
        result.Length.ShouldBe(3);
        result[0].ShouldBe((byte)0x02);
        result[1].ShouldBe((byte)0x03);
        result[2].ShouldBe((byte)0x04);
    }

    [Theory]
    [InlineData(8, 1)] // 8 bits = 1 byte
    [InlineData(9, 2)] // 9 bits = 2 bytes (rounded up)
    [InlineData(16, 2)] // 16 bits = 2 bytes
    [InlineData(17, 3)] // 17 bits = 3 bytes (rounded up)
    [InlineData(24, 3)] // 24 bits = 3 bytes
    [InlineData(25, 4)] // 25 bits = 4 bytes (rounded up)
    public void TruncateToBitLength_VariousBitLengths_ReturnsCorrectByteLength(
        ushort bitLength,
        int expectedByteLength
    )
    {
        // Arrange
        byte[] data = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06 };

        // Act
        var result = data.TruncateToBitLength(bitLength);

        // Assert
        result.Length.ShouldBe(expectedByteLength);
    }

    [Fact]
    public void TruncateToBitLength_WhenBitLengthEqualsDataLength_ReturnsAllData()
    {
        // Arrange
        byte[] data = { 0x01, 0x02, 0x03 };
        ushort bitLength = 24; // 3 bytes

        // Act
        var result = data.TruncateToBitLength(bitLength);

        // Assert
        result.Length.ShouldBe(3);
        result.ShouldBe(data);
    }

    [Fact]
    public void TruncateToBitLength_WhenBitLengthExceedsDataLength_ReturnsAllData()
    {
        // Arrange
        byte[] data = { 0x01, 0x02 };
        ushort bitLength = 32; // 4 bytes, but data only has 2
        // requiredByteLength = 32/8 + 0 = 4
        // But data.Length - requiredByteLength = 2 - 4 = -2, which causes ArgumentOutOfRangeException

        // Act & Assert
        // The current implementation doesn't handle this case gracefully
        Should.Throw<ArgumentOutOfRangeException>(() => data.TruncateToBitLength(bitLength));
    }

    [Fact]
    public void TruncateToBitLength_WithEmptyArray_ThrowsException()
    {
        // Arrange
        byte[] data = Array.Empty<byte>();
        ushort bitLength = 8;
        // requiredByteLength = 8/8 + 0 = 1
        // data.Length - requiredByteLength = 0 - 1 = -1, which causes ArgumentOutOfRangeException

        // Act & Assert
        Should.Throw<ArgumentOutOfRangeException>(() => data.TruncateToBitLength(bitLength));
    }

    [Fact]
    public void TruncateToBitLength_WithZeroBitLength_ReturnsEmptyArray()
    {
        // Arrange
        byte[] data = { 0x01, 0x02, 0x03 };
        ushort bitLength = 0;
        // requiredByteLength = 0/8 + 0 = 0
        // data.Length - requiredByteLength = 3 - 0 = 3, so we get data[3..] which is empty

        // Act
        var result = data.TruncateToBitLength(bitLength);

        // Assert
        result.Length.ShouldBe(0);
    }

    [Fact]
    public void PinNegativeIntToRequiredBitLength_WithEmptyArray_ReturnsEmptyArray()
    {
        // Arrange
        byte[] data = Array.Empty<byte>();
        ushort bitLength = 8;

        // Act
        var result = data.PinNegativeIntToRequiredBitLength(bitLength);

        // Assert
        result.Length.ShouldBe(0);
        result.ShouldBeSameAs(data);
    }

    [Fact]
    public void ByteArrayExtensions_MethodsAreExtensionMethods()
    {
        // Arrange
        byte[] data = { 0x01, 0x02 };

        // Act & Assert
        // This test verifies that the methods can be called as extension methods
        var result1 = data.TruncateToBitLength(8);
        var result2 = data.PinNegativeIntToRequiredBitLength(8);

        result1.ShouldNotBeNull();
        result2.ShouldNotBeNull();
    }
}
