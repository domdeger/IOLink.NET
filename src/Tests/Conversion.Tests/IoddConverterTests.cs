using System.Buffers.Text;

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

        var result = converter.Convert(testRecord, new byte[] { /*1111 1111*/ 0xff });
        result.Should().BeOfType<IEnumerable<(string, object)>>();
    }

    [Fact]
    public void CanConvertRecordT()
    {
        var data = Convert.FromBase64String("AB0AHAAdABMALgATAC4=");
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

        var result = converter.Convert(testRecord, data);

        result.Should().BeOfType<IEnumerable<(string, object)>>();
    }
}