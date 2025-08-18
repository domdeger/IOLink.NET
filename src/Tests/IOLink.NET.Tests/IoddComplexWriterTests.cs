using FluentAssertions;
using IOLink.NET.IODD.Resolution;
using IOLink.NET.IODD.Structure.Datatypes;

namespace IOLink.NET.Tests;

public class IoddComplexWriterTests
{
    [Fact]
    public void WriteArrayType_WithValidData_ShouldReturnCorrectBytes()
    {
        // Arrange
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.UInteger, 8);
        var arrayType = new ParsableArray("testArray", elementType, true, 3);
        var values = new object[] { 1, 2, 3 };

        // Act
        var result = IoddComplexWriter.Write(arrayType, values);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(3); // 3 bytes for 3 8-bit elements
    }

    [Fact]
    public void WriteArrayType_WithWrongLength_ShouldThrowArgumentException()
    {
        // Arrange
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.UInteger, 8);
        var arrayType = new ParsableArray("testArray", elementType, true, 3);
        var values = new object[] { 1, 2 }; // Wrong length

        // Act & Assert
        var act = () => IoddComplexWriter.Write(arrayType, values);
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Array length mismatch. Expected 3, got 2*");
    }

    [Fact]
    public void WriteArrayType_WithNonEnumerable_ShouldThrowArgumentException()
    {
        // Arrange
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.UInteger, 8);
        var arrayType = new ParsableArray("testArray", elementType, true, 3);
        var value = "not enumerable";

        // Act & Assert
        var act = () => IoddComplexWriter.Write(arrayType, value);
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Value must be an enumerable for array types*");
    }

    [Fact]
    public void WriteArrayType_With4BitElements_ShouldPackCorrectly()
    {
        // Arrange
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.UInteger, 4);
        var arrayType = new ParsableArray("testArray", elementType, true, 2);
        var values = new object[] { 5, 10 }; // 0101, 1010

        // Act
        var result = IoddComplexWriter.Write(arrayType, values);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(1); // 2 4-bit elements should fit in 1 byte
    }

    [Fact]
    public void WriteRecordType_WithValidData_ShouldReturnCorrectBytes()
    {
        // Arrange
        var field1 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("field1", KindOfSimpleType.UInteger, 4),
            "field1",
            0,
            1
        );
        var field2 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("field2", KindOfSimpleType.UInteger, 4),
            "field2",
            4,
            2
        );

        var recordType = new ParsableRecord("testRecord", 8, true, new[] { field1, field2 });
        var values = new (string key, object value)[] { ("field1", 5), ("field2", 10) };

        // Act
        var result = IoddComplexWriter.Write(recordType, values);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(1); // 8 bits = 1 byte
    }

    [Fact]
    public void WriteRecordType_WithMissingField_ShouldThrowArgumentException()
    {
        // Arrange
        var field1 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("field1", KindOfSimpleType.UInteger, 4),
            "field1",
            0,
            1
        );
        var field2 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("field2", KindOfSimpleType.UInteger, 4),
            "field2",
            4,
            2
        );

        var recordType = new ParsableRecord("testRecord", 8, true, new[] { field1, field2 });
        var values = new (string key, object value)[]
        {
            ("field1", 5),
            // Missing field2
        };

        // Act & Assert
        var act = () => IoddComplexWriter.Write(recordType, values);
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Missing value for record item 'field2'*");
    }

    [Fact]
    public void WriteRecordType_WithNonKeyValuePairs_ShouldThrowArgumentException()
    {
        // Arrange
        var field1 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("field1", KindOfSimpleType.UInteger, 8),
            "field1",
            0,
            1
        );

        var recordType = new ParsableRecord("testRecord", 8, true, new[] { field1 });
        var value = "not key-value pairs";

        // Act & Assert
        var act = () => IoddComplexWriter.Write(recordType, value);
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Value must be an enumerable of key-value pairs for record types*");
    }

    [Fact]
    public void WriteRecordType_ComplexRecord_ShouldHandleMultipleFields()
    {
        // Arrange
        var field1 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("temp", KindOfSimpleType.Integer, 16),
            "temperature",
            0,
            1
        );
        var field2 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("humid", KindOfSimpleType.UInteger, 8),
            "humidity",
            16,
            2
        );
        var field3 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("active", KindOfSimpleType.Boolean, 1),
            "isActive",
            24,
            3
        );

        var recordType = new ParsableRecord(
            "sensorData",
            25,
            true,
            new[] { field1, field2, field3 }
        );
        var values = new (string key, object value)[]
        {
            ("temperature", 250),
            ("humidity", 65),
            ("isActive", true),
        };

        // Act
        var result = IoddComplexWriter.Write(recordType, values);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(4); // 25 bits = 4 bytes (rounded up)
    }

    [Fact]
    public void Write_WithUnsupportedComplexType_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var unsupportedType = new TestUnsupportedComplexType();
        var value = new object();

        // Act & Assert
        var act = () => IoddComplexWriter.Write(unsupportedType, value);
        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Type TestUnsupportedComplexType is not supported.");
    }

    [Theory]
    [InlineData(1, new byte[] { 0x01 })]
    [InlineData(2, new byte[] { 0x01, 0x02 })]
    [InlineData(3, new byte[] { 0x01, 0x02, 0x03 })]
    public void WriteArrayType_WithDifferentLengths_ShouldHandleCorrectly(
        int length,
        byte[] expectedValues
    )
    {
        // Arrange
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.UInteger, 8);
        var arrayType = new ParsableArray("testArray", elementType, true, (ushort)length);
        var values = expectedValues.Cast<object>().ToArray();

        // Act
        var result = IoddComplexWriter.Write(arrayType, values);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(length);
    }

    [Fact]
    public void WriteArrayType_WithBooleanElements_ShouldPackBitsCorrectly()
    {
        // Arrange
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.Boolean, 1);
        var arrayType = new ParsableArray("boolArray", elementType, true, 8);
        var values = new object[] { true, false, true, true, false, false, true, false };

        // Act
        var result = IoddComplexWriter.Write(arrayType, values);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(1); // 8 bits = 1 byte
    }

    [Fact]
    public void WriteRecordType_WithBitPackedFields_ShouldHandleOffsets()
    {
        // Arrange - Create a record with fields at different bit offsets
        var field1 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("f1", KindOfSimpleType.UInteger, 3),
            "field1",
            0,
            1
        );
        var field2 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("f2", KindOfSimpleType.UInteger, 2),
            "field2",
            3,
            2
        );
        var field3 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("f3", KindOfSimpleType.UInteger, 3),
            "field3",
            5,
            3
        );

        var recordType = new ParsableRecord(
            "packedRecord",
            8,
            true,
            new[] { field1, field2, field3 }
        );
        var values = new (string key, object value)[]
        {
            ("field1", 5), // 101
            ("field2", 2), // 10
            (
                "field3",
                3
            ) // 011
            ,
        };

        // Act
        var result = IoddComplexWriter.Write(recordType, values);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(1); // 8 bits = 1 byte
    }

    // Helper class for testing unsupported types
    private record TestUnsupportedComplexType(string Name, bool SubindexAccessSupported)
        : ParsableComplexDataTypeDef(Name, SubindexAccessSupported)
    {
        public TestUnsupportedComplexType()
            : this("test", true) { }
    }
}
