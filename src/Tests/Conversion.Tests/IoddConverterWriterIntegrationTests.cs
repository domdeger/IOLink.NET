using System.Xml.Linq;
using FluentAssertions;
using IOLinkNET.Conversion;
using IOLinkNET.IODD;
using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Structure.Datatypes;

namespace Conversion.Tests;

public class IoddConverterWriterIntegrationTests
{
    [Fact]
    public void ConvertToBytes_RealDeviceProcessData_ShouldRoundTripCorrectly()
    {
        // Arrange
        var originalData = Convert.FromBase64String("gAEDAAAAAAAAgAA=");
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load("TestData/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml"));
        var converter = new IoddConverter();

        var pdResolver = new ProcessDataTypeResolver(device);
        var convertibleType = pdResolver.ResolveProcessDataIn()!;

        // First convert from bytes to objects
        var convertedObjects = converter.Convert(convertibleType, originalData) as IEnumerable<(string, object)>;
        
        // Act - Convert back to bytes
        var resultBytes = converter.ConvertToBytes(convertedObjects!, convertibleType);

        // Assert
        resultBytes.Should().BeEquivalentTo(originalData);
    }

    [Fact]
    public void ConvertToBytes_SimpleRecordData_ShouldMatchExpectedFormat()
    {
        // Arrange
        var converter = new IoddConverter();
        var recordItem1 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("uint_1", KindOfSimpleType.UInteger, 4), 
            "field1", 0, 1);
        var recordItem2 = new ParsableRecordItem(
            new ParsableSimpleDatatypeDef("uint_2", KindOfSimpleType.UInteger, 4), 
            "field2", 4, 1);
        var testRecord = new ParsableRecord("TestRecord", 8, true, new[] { recordItem1, recordItem2 });

        var inputData = new (string, object)[] 
        {
            ("field1", 15), // 1111 in binary
            ("field2", 15)  // 1111 in binary
        };

        // Act
        var result = converter.ConvertToBytes(inputData, testRecord);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(1);
        result[0].Should().Be(0xFF); // 11111111 in binary
    }

    [Fact]
    public void ConvertToBytes_ComplexRecordFromIODD_ShouldHandleCorrectly()
    {
        // Arrange
        var converter = new IoddConverter();
        var testRecord = new ParsableRecord("DemoRecord", 112, true, new[] {
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Device_Temp", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Device_Temp", 96, 1),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Startup", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Startup", 80, 2),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Startup", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Startup", 64, 3),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Minimum_Device_Temp_Lifetime", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Minimum_Device_Temp_Lifetime", 48, 4),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Maximum_Device_Temp_Lifetime", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Maximum_Device_Temp_Lifetime", 32, 5),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Reset", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Reset", 16, 6),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Reset", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Reset", 0, 7),
        });

        var inputData = new (string, object)[] 
        {
            ("TI_VAR_Device_Temp_Device_Temp", 29),
            ("TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Startup", 28),
            ("TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Startup", 29),
            ("TI_VAR_Device_Temp_Minimum_Device_Temp_Lifetime", 19),
            ("TI_VAR_Device_Temp_Maximum_Device_Temp_Lifetime", 46),
            ("TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Reset", 19),
            ("TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Reset", 46)
        };

        // Act
        var result = converter.ConvertToBytes(inputData, testRecord);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(14); // 112 bits = 14 bytes
    }

    [Fact]
    public void ConvertToBytes_ArrayData_ShouldHandleMultipleElements()
    {
        // Arrange
        var converter = new IoddConverter();
        var elementType = new ParsableSimpleDatatypeDef("element", KindOfSimpleType.UInteger, 16);
        var arrayType = new ParsableArray("testArray", elementType, true, 4);
        var inputData = new object[] { 1000, 2000, 3000, 4000 };

        // Act
        var result = converter.ConvertToBytes(inputData, arrayType);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(8); // 4 * 16 bits = 64 bits = 8 bytes
    }

    [Fact]
    public void ConvertToBytes_MixedDataTypes_ShouldHandleCorrectly()
    {
        // Arrange
        var converter = new IoddConverter();
        var testRecord = new ParsableRecord("MixedRecord", 33, true, new[] {
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("intField", KindOfSimpleType.Integer, 16), "intField", 0, 1),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("uintField", KindOfSimpleType.UInteger, 8), "uintField", 16, 2),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("boolField", KindOfSimpleType.Boolean, 1), "boolField", 24, 3),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("smallUint", KindOfSimpleType.UInteger, 8), "smallUint", 25, 4)
        });

        var inputData = new (string, object)[] 
        {
            ("intField", -1000),
            ("uintField", 200),
            ("boolField", true),
            ("smallUint", 50)
        };

        // Act
        var result = converter.ConvertToBytes(inputData, testRecord);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(5); // 33 bits = 5 bytes (rounded up)
    }

    [Fact]
    public void ConvertToBytes_FloatData_ShouldHandleCorrectly()
    {
        // Arrange
        var converter = new IoddConverter();
        var floatType = new ParsableSimpleDatatypeDef("floatValue", KindOfSimpleType.Float, 32);
        var value = 3.14159f;

        // Act
        var result = converter.ConvertToBytes(value, floatType);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(4); // 32 bits = 4 bytes
    }

    [Fact]
    public void ConvertToBytes_StringData_ShouldHandleEncoding()
    {
        // Arrange
        var converter = new IoddConverter();
        var stringType = new ParsableStringDef("textValue", 20, StringTEncoding.UTF8);
        var value = "Test String";

        // Act
        var result = converter.ConvertToBytes(value, stringType);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(value.Length);
    }

    [Fact]
    public void ConvertToBytes_EdgeCaseBitLengths_ShouldHandleCorrectly()
    {
        // Test with odd bit lengths that don't align to byte boundaries
        // Arrange
        var converter = new IoddConverter();
        var testRecord = new ParsableRecord("EdgeCaseRecord", 13, true, new[] {
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("field1", KindOfSimpleType.UInteger, 5), "field1", 0, 1),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("field2", KindOfSimpleType.UInteger, 3), "field2", 5, 2),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("field3", KindOfSimpleType.UInteger, 5), "field3", 8, 3)
        });

        var inputData = new (string, object)[] 
        {
            ("field1", 31), // 5 bits: 11111
            ("field2", 7),  // 3 bits: 111
            ("field3", 15)  // 5 bits: 01111
        };

        // Act
        var result = converter.ConvertToBytes(inputData, testRecord);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(2); // 13 bits = 2 bytes (rounded up)
    }
}
