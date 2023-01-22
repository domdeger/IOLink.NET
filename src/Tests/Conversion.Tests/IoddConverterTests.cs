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
}