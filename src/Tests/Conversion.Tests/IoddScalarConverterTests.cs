using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.IODD.Resolution;

namespace Conversion.Tests;

public class IoddScalarConverterTests
{
    [Fact]
    public void CanConvert4BitPositiveInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 4);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b0000_0100 });
        _ = result.Should().Be(4);
    }

    [Fact]
    public void CanConvert4BitNegativeInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 4);

        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b0000_1100 });

        _ = result.Should().Be(-4);
    }

    [Fact]
    public void CanConvert17BitPositiveInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 17);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b00000000, 0b01101110, 0b01011010 });
        _ = result.Should().Be(28250);
    }

    [Fact]
    public void CanConvert17BitNegativeInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 17);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b00000001, 0b10010001, 0b10100110 });
        _ = result.Should().Be(-28250);
    }

    [Fact]
    public void CanConvert32BitNegativeInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 32);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b11111110, 0b01010000, 0b11110000, 0b00001100 });
        _ = result.Should().Be(-28250100);
        _ = result.Should().BeOfType<int>();
    }

    [Fact]
    public void CanConvert33BitNegativeInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 33);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b0000001, 0b11111110, 0b01010000, 0b11110000, 0b00001100 });
        _ = result.Should().Be(-28250100);
        _ = result.Should().BeOfType<long>();
    }

    [Fact]
    public static void CanConvert12BitUInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.UInteger, 12);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b00000000, 0b0000_0100 });
        _ = result.Should().Be(4);
        _ = result.Should().BeOfType<ushort>();
    }

    [Fact]
    public static void CanConvert4BitUInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.UInteger, 4);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b0000_0100 });
        _ = result.Should().Be(4);
        _ = result.Should().BeOfType<byte>();
    }

    [Fact]
    public static void CanConvert17BitUInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.UInteger, 17);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b00000001, 0b01101110, 0b01011010 });
        _ = result.Should().Be(93786);
        _ = result.Should().BeOfType<uint>();
    }

    [Fact]
    public static void CanConvert48BitUInteger()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.UInteger, 48);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b00000000, 0b00000000, 0b00000000, 0b00000001, 0b01101110, 0b01011010 });
        _ = result.Should().Be(93786);
        _ = result.Should().BeOfType<ulong>();
    }

    [Fact]
    public static void CanConvertPositiveFloat32()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Float, 32);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b00111111, 0b01000000, 0b00000000, 0b00000000 });
        _ = result.Should().Be(0.75);
        _ = result.Should().BeOfType<float>();
    }

    [Fact]
    public static void CanConvertNegativeFloat32()
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Float, 32);
        object result = IoddScalarConverter.Convert(typeDef, new byte[] { 0b10111111, 0b01000000, 0b00000000, 0b00000000 });
        _ = result.Should().Be(-0.75);
        _ = result.Should().BeOfType<float>();
    }


}