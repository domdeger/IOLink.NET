using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.IODD.Resolution;

namespace Conversion.Tests;

public class IoddConverterTests
{
    [Fact]
    public void CanConvertWithPaddedBitOffset()
    {
        var converter = new IoddConverter();
        var recordItem = new ParsableRecordItem(new ParsableSimpleDatatypeDef("uint_1", KindOfSimpleType.UInteger, 4), "Test", 0, 1);
        var recordItem1 = new ParsableRecordItem(new ParsableSimpleDatatypeDef("uint_2", KindOfSimpleType.UInteger, 4), "Test", 4, 1);
        var testRecord = new ParsableRecord("Test", new[] { recordItem, recordItem1 });

        object result = converter.Convert(testRecord, new byte[] { /*1111 1111*/ 0xff });
        _ = result.Should().BeAssignableTo<IEnumerable<(string, object)>>();
    }

    [Fact]
    public void CanConvertRecordT()
    {
        byte[] data = Convert.FromBase64String("AB0AHAAdABMALgATAC4=");
        byte[] binaryData = new byte[]
                            { 0b00000000, 0b00011101, 0b00000000, 0b00011100, 0b00000000, 0b00011101, 0b00000000,
                              0b00010011, 0b00000000, 0b00101110, 0b00000000, 0b00010011, 0b00000000, 0b00101110 };

        _ = data.Should().BeEquivalentTo(binaryData);
        var converter = new IoddConverter();

        var testRecord = new ParsableRecord("DemoRecord", new[] {
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Device_Temp", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Device_Temp", 96, 1),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Startup", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Startup", 80, 2),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Startup", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Startup", 64, 3),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Minimum_Device_Temp_Lifetime", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Minimum_Device_Temp_Lifetime", 48, 4),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Maximum_Device_Temp_Lifetime", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Maximum_Device_Temp_Lifetime", 32, 5),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Reset", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Minimum_Device_Temp_Since_Reset", 16, 6),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Reset", KindOfSimpleType.Integer, 16), "TI_VAR_Device_Temp_Maximum_Device_Temp_Since_Reset", 0, 7),
         });

        object result = converter.Convert(testRecord, data);

        _ = result.Should().BeAssignableTo<IEnumerable<(string, object)>>();
    }

    [Fact]
    public void CanConvertArray()
    {
        var arrayDefinition = new ParsableArray("V_SomeArray", new ParsableSimpleDatatypeDef("uint_1", KindOfSimpleType.UInteger, 3), 5);
        var converter = new IoddConverter();

        byte[] data = new byte[] { 0b10101101, 0b00111101 };
        object result = converter.Convert(arrayDefinition, data);

        _ = result.Should().BeAssignableTo<IEnumerable<(string, object)>>();
        var array = result as IEnumerable<(string, object)>;

        _ = array.Should().HaveCount(5);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_0" && (byte)x.Item2 == 5);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_1" && (byte)x.Item2 == 5);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_2" && (byte)x.Item2 == 6);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_3" && (byte)x.Item2 == 6);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_4" && (byte)x.Item2 == 3);
    }
}