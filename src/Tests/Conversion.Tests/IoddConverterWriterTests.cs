using FluentAssertions;
using IOLinkNET.Conversion;
using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Structure.Datatypes;

namespace Conversion.Tests;

public class IoddConverterWriterTests
{
    private readonly IoddConverter _converter = new();

    [Fact]
    public void ConvertToBytes_WithSimpleType_ShouldUseScalarWriter()
    {
        // Arrange
        var typeDef = new ParsableSimpleDatatypeDef("test", KindOfSimpleType.UInteger, 8);
        var value = 42;

        // Act
        var result = _converter.ConvertToBytes(value, typeDef);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(1);
        result[0].Should().Be(42);
    }

    [Fact]
    public void ConvertToBytes_WithComplexArrayType_ShouldUseComplexWriter()
    {
        // Arrange
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.UInteger, 8);
        var arrayType = new ParsableArray("testArray", elementType, true, 3);
        var values = new object[] { 10, 20, 30 };

        // Act
        var result = _converter.ConvertToBytes(values, arrayType);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(3);
    }

    [Fact]
    public void ConvertToBytes_WithComplexRecordType_ShouldUseComplexWriter()
    {
        // Arrange
        var field1 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("field1", KindOfSimpleType.UInteger, 8),
            "field1", 0, 1);
        var field2 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("field2", KindOfSimpleType.UInteger, 8),
            "field2", 8, 2);
        
        var recordType = new ParsableRecord("testRecord", 16, true, new[] { field1, field2 });
        var values = new (string key, object value)[] 
        {
            ("field1", 100),
            ("field2", 200)
        };

        // Act
        var result = _converter.ConvertToBytes(values, recordType);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(2);
    }

    [Fact]
    public void ConvertToBytes_WithUnsupportedType_ShouldThrowNotImplementedException()
    {
        // Arrange
        var unsupportedType = new TestUnsupportedDatatype();
        var value = new object();

        // Act & Assert
        var act = () => _converter.ConvertToBytes(value, unsupportedType);
        act.Should().Throw<NotImplementedException>();
    }

    [Theory]
    [InlineData(KindOfSimpleType.Integer, 16, -1000)]
    [InlineData(KindOfSimpleType.UInteger, 16, 1000)]
    [InlineData(KindOfSimpleType.Float, 32, 3.14f)]
    [InlineData(KindOfSimpleType.Boolean, 1, true)]
    public void ConvertToBytes_WithDifferentSimpleTypes_ShouldHandleCorrectly(
        KindOfSimpleType type, ushort bitLength, object value)
    {
        // Arrange
        var typeDef = new ParsableSimpleDatatypeDef("test", type, bitLength);

        // Act
        var result = _converter.ConvertToBytes(value, typeDef);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public void ConvertToBytes_StringType_ShouldHandleCorrectly()
    {
        // Arrange
        var typeDef = new ParsableStringDef("test", 10, StringTEncoding.ASCII);
        var value = "Hello";

        // Act
        var result = _converter.ConvertToBytes(value, typeDef);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F });
    }

    [Fact]
    public void ConvertToBytes_RoundTrip_ShouldProduceSameResult()
    {
        // Arrange
        var typeDef = new ParsableSimpleDatatypeDef("test", KindOfSimpleType.Integer, 16);
        var originalValue = -12345;

        // Act
        var bytes = _converter.ConvertToBytes(originalValue, typeDef);
        var convertedBack = _converter.Convert(typeDef, bytes);

        // Assert
        convertedBack.Should().Be(originalValue);
    }

    [Fact]
    public void ConvertToBytes_ComplexRoundTrip_ShouldProduceSameResult()
    {
        // Arrange
        var field1 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("temperature", KindOfSimpleType.Integer, 16),
            "temperature", 0, 1);
        var field2 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("humidity", KindOfSimpleType.UInteger, 8),
            "humidity", 16, 2);
        
        var recordType = new ParsableRecord("sensorData", 24, true, new[] { field1, field2 });
        var originalValues = new (string key, object value)[] 
        {
            ("temperature", -250),
            ("humidity", 65)
        };

        // Act
        var bytes = _converter.ConvertToBytes(originalValues, recordType);
        var convertedBack = _converter.Convert(recordType, bytes) as IEnumerable<(string, object)>;

        // Assert
        convertedBack.Should().NotBeNull();
        var resultDict = convertedBack!.ToDictionary(x => x.Item1, x => x.Item2);
        resultDict["temperature"].Should().Be(-250);
        resultDict["humidity"].Should().Be(65);
    }

    [Fact]
    public void ConvertToBytes_ArrayRoundTrip_ShouldProduceSameResult()
    {
        // Arrange
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.Integer, 16);
        var arrayType = new ParsableArray("testArray", elementType, true, 3);
        var originalValues = new object[] { -100, 0, 100 };

        // Act
        var bytes = _converter.ConvertToBytes(originalValues, arrayType);
        var convertedBack = _converter.Convert(arrayType, bytes) as IEnumerable<(string, object)>;

        // Assert
        convertedBack.Should().NotBeNull();
        var resultList = convertedBack!.Select(x => x.Item2).ToArray();
        resultList.Should().BeEquivalentTo(originalValues);
    }

    // Helper class for testing unsupported types
    private record TestUnsupportedDatatype(string Name, bool SubindexAccessSupported) 
        : ParsableDatatype(Name, SubindexAccessSupported)
    {
        public TestUnsupportedDatatype() : this("test", true) { }
    }
}
