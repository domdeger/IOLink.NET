using IOLink.NET.IODD.Resolution;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.Tests;

public class IoddScalarWriterTests
{
    [Theory]
    [InlineData(4, new byte[] { 0b0000_0100 })]
    [InlineData(7, new byte[] { 0b0000_0111 })]
    [InlineData(-4, new byte[] { 0b0000_1100 })]
    [InlineData(-7, new byte[] { 0b0000_1001 })]
    public void CanWrite4BitInteger(int value, byte[] expected)
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 4);
        byte[] result = IoddScalarWriter.Write(typeDef, value);
        result.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(28250, new byte[] { 0b00000000, 0b01101110, 0b01011010 })]
    [InlineData(-28250, new byte[] { 0b00000001, 0b10010001, 0b10100110 })]
    public void CanConvert17BitInteger(int value, byte[] expected)
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 17);
        byte[] result = IoddScalarWriter.Write(typeDef, value);
        result.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(-28250100, new byte[] { 0b11111110, 0b01010000, 0b11110000, 0b00001100 })]
    public void CanConvert32BitNegativeInteger(int value, byte[] expected)
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 32);
        byte[] result = IoddScalarWriter.Write(typeDef, value);
        result.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(
        -28250100,
        new byte[] { 0b0000001, 0b11111110, 0b01010000, 0b11110000, 0b00001100 }
    )]
    public void CanConvert33BitNegativeInteger(int value, byte[] expected)
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Integer, 33);
        byte[] result = IoddScalarWriter.Write(typeDef, value);
        result.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(12, 4, new byte[] { 0b00000000, 0b0000_0100 })]
    [InlineData(4, 4, new byte[] { 0b0000_0100 })]
    [InlineData(17, 93786, new byte[] { 0b00000001, 0b01101110, 0b01011010 })]
    [InlineData(
        48,
        93786,
        new byte[] { 0b00000000, 0b00000000, 0b00000000, 0b00000001, 0b01101110, 0b01011010 }
    )]
    [InlineData(
        64,
        ulong.MaxValue,
        new byte[]
        {
            0b1111_1111,
            0b1111_1111,
            0b1111_1111,
            0b1111_1111,
            0b1111_1111,
            0b1111_1111,
            0b1111_1111,
            0b1111_1111,
        }
    )]
    public static void CanConvertUInteger(ushort bitLength, ulong value, byte[] expected)
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.UInteger, bitLength);
        byte[] result = IoddScalarWriter.Write(typeDef, value);
        result.ShouldBeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(0.75, new byte[] { 0b00111111, 0b01000000, 0b00000000, 0b00000000 })]
    [InlineData(-0.75, new byte[] { 0b10111111, 0b01000000, 0b00000000, 0b00000000 })]
    public static void CanConvertPositiveFloat32(float value, byte[] expected)
    {
        var typeDef = new ParsableSimpleDatatypeDef("intp", KindOfSimpleType.Float, 32);
        byte[] result = IoddScalarWriter.Write(typeDef, value);
        result.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public static void CanConvertAsciiString()
    {
        var typeDef = new ParsableStringDef("intp", 32, StringTEncoding.ASCII);
        byte[] result = IoddScalarWriter.Write(typeDef, "Hello");
        result.ShouldBeEquivalentTo(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F });
    }

    [Fact]
    public static void CanConvertUtf8String()
    {
        var typeDef = new ParsableStringDef("intp", 32, StringTEncoding.UTF8);
        byte[] result = IoddScalarWriter.Write(typeDef, "Hello😀");
        result.ShouldBeEquivalentTo(
            new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F, 0xF0, 0x9F, 0x98, 0x80 }
        );
    }
}
