using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Structure.Datatypes;

namespace Conversion.Tests;

public class IoddConverterTests
{
    [Fact]
    public void CanConvertWithPaddedBitOffset()
    {
        var converter = new IoddConverter();
        var recordItem = new ParsableRecordItem(new ParsableSimpleDatatypeDef("uint_1", KindOfSimpleType.UInteger, 4), "Test", 0, 1);
        var recordItem1 = new ParsableRecordItem(new ParsableSimpleDatatypeDef("uint_2", KindOfSimpleType.UInteger, 4), "Test", 4, 1);
        var testRecord = new ParsableRecord("Test", 8, true, new[] { recordItem, recordItem1 });

        object result = converter.Convert(testRecord, [/*1111 1111*/ 0xff]);
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

        var testRecord = new ParsableRecord("DemoRecord", 112, true, new[] {
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
    public void CanConvertRecordFromIODDSystemDescription()
    {
        /*
            This test case verifies that the recordt conversion is working correctly for the record definition from the 
            IO-Link system description: https://io-link.com/share/Downloads/Package-2020/IOL-Interface-Spec_10002_V113_Jun19.pdf 
            page 280.
        */
        var converter = new IoddConverter();

        var recordDef = new ParsableRecord("IOLinkDemoRecord", 40, true, new[] {
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("NewBit", KindOfSimpleType.Boolean, 1), "NewBit", 32, 1),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("DR4", KindOfSimpleType.Boolean, 1), "DR4", 33, 2),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("CR3", KindOfSimpleType.Boolean, 1), "CR3", 34, 3),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("CR2", KindOfSimpleType.Boolean, 1), "CR2", 35, 4),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("Control", KindOfSimpleType.Boolean, 1), "Control", 38, 5),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("Setpoint", KindOfSimpleType.OctetString, 16), "Setpoint", 16, 6),
            new ParsableRecordItem(new ParsableStringDef("Unit", 8, StringTEncoding.ASCII), "Unit", 8, 7),
            new ParsableRecordItem(new ParsableSimpleDatatypeDef("Enable", KindOfSimpleType.OctetString, 8), "Enable", 0, 8),
        });
        var data = new byte[] { 0b01001001, 0xF8, 0x23, 0x41, 0xC3 };
        var result = converter.Convert(recordDef, data);

        result.Should().NotBeNull();
        result.Should().BeAssignableTo<IEnumerable<(string, object)>>();
        var record = result as IEnumerable<(string, object)>;
        record.Should().HaveCount(8);
        record.Should().Contain(x => x.Item1 == "NewBit" && (bool)x.Item2 == true);
        record.Should().Contain(x => x.Item1 == "DR4" && (bool)x.Item2 == false);
        record.Should().Contain(x => x.Item1 == "CR3" && (bool)x.Item2 == false);
        record.Should().Contain(x => x.Item1 == "CR2" && (bool)x.Item2 == true);
        record.Should().Contain(x => x.Item1 == "Control" && (bool)x.Item2 == true);
        record.Should().Contain(x => x.Item1 == "Setpoint" && (string)x.Item2 == "F823");
        record.Should().Contain(x => x.Item1 == "Unit" && (string)x.Item2 == "A");
        record.Should().Contain(x => x.Item1 == "Enable" && (string)x.Item2 == "C3");
    }

    [Fact]
    public void CanConvertArray()
    {
        /*
            This test case verifies that the array conversion is working correctly for the array definition from the 
            IO-Link system description: https://io-link.com/share/Downloads/Package-2020/IOL-Interface-Spec_10002_V113_Jun19.pdf 
            page 278.
         */
        var arrayDefinition = new ParsableArray("V_SomeArray", new ParsableSimpleDatatypeDef("uint_1", KindOfSimpleType.UInteger, 3), true, 5);
        var converter = new IoddConverter();

        byte[] data = [0b0101101, 0b00111101];
        object result = converter.Convert(arrayDefinition, data);

        _ = result.Should().BeAssignableTo<IEnumerable<(string, object)>>();
        var array = result as IEnumerable<(string, object)>;

        _ = array.Should().HaveCount(5);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_1" && (byte)x.Item2 == 2);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_2" && (byte)x.Item2 == 6);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_3" && (byte)x.Item2 == 4);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_4" && (byte)x.Item2 == 7);
        _ = array.Should().Contain(x => x.Item1 == "V_SomeArray_5" && (byte)x.Item2 == 5);
    }
}