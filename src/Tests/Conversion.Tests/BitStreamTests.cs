using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.Formatting;

namespace Conversion.Tests;

public class BitStreamTests
{
    [Fact]
    public void ReadsWholeByte()
    {
        var payload = new byte[] { 0b11100101 };
        var bitStream = new BitStream(payload);
        var result = bitStream.GetBitSpan(0, 8).ToArray().First();
        result.Should().Be(payload[0]);
    }

    [Fact]
    public void GetsBitsInMiddleOfFirstByte()
    {
        var payload = new byte[] { 0b0111000 };
        var bitStream = new BitStream(payload);
        var result = bitStream.GetBitSpan(3, 3).ToArray().First();
        result.Should().Be(0b111);
    }

    [Fact]
    public void GetsBitsFromBeginningOfFirstByte()
    {
        var payload = new byte[] { 0b01100101 };
        var bitStream = new BitStream(payload);
        var result = bitStream.GetBitSpan(0, 3).ToArray().First();
        result.Should().Be(0b101);
    }

    [Fact]
    public void GetsBitsFromEndOfFirstByte()
    {
        var payload = new byte[] { 0b10100000 };
        var bitStream = new BitStream(payload);
        var result = bitStream.GetBitSpan(5, 3).ToArray().First();
        result.Should().Be(0b101);
    }

    [Fact]
    public void GetsBitsFromBeginningOfSecondByte()
    {
        var payload = new byte[] { 0b11100000, 0b10100101 };
        var bitStream = new BitStream(payload);
        var result = bitStream.GetBitSpan(8, 3).ToArray().First();
        result.Should().Be(0b101);
    }
}