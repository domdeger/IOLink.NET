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

}