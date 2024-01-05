using System.Xml.Linq;

using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.IODD;
using IOLinkNET.IODD.Resolution;

namespace Conversion.Tests;

public class IoddConverterIntegrationTests
{
    [Fact]
    public void ShouldConvertProcessDataForRealDevice393780()
    {
        var data = Convert.FromBase64String("gAEDAAAAAAAAgAA=");
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load("TestData/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml"));
        var converter = new IoddConverter();

        var pdResolver = new ProcessDataTypeResolver(device);
        var convertibleType = pdResolver.ResolveProcessDataIn()!;

        var result = (converter.ConvertFromIoLink(convertibleType, data) as List<(string, object)>)!.ToDictionary(x => x.Item1, y => y.Item2);

        result.Should().ContainKey("TI_PDObject_75").WhoseValue.Should().BeOfType<bool>().And.Be(false);
        result.Should().ContainKey("TI_PDObject_55").WhoseValue.Should().BeOfType<bool>().And.Be(false);
        result.Should().ContainKey("TI_PDObject_78").WhoseValue.Should().BeOfType<bool>().And.Be(false);
        result.Should().ContainKey("TI_PDObject_33").WhoseValue.Should().BeOfType<bool>().And.Be(false);
        result.Should().ContainKey("TI_PDObject_47").WhoseValue.Should().BeOfType<bool>().And.Be(false);
        result.Should().ContainKey("TI_PDI_FirstBitHeader").WhoseValue.Should().Be(128);
        result.Should().ContainKey("TI_PDI_FirstByte").WhoseValue.Should().Be(1);
        result.Should().ContainKey("TI_PDI_SecondByte").WhoseValue.Should().Be(3);
        result.Should().ContainKey("TI_PDI_ThirdByte").WhoseValue.Should().Be(0);
        result.Should().ContainKey("TI_PDI_FourthByte").WhoseValue.Should().Be(0);
        result.Should().ContainKey("TI_PDI_FifthByte").WhoseValue.Should().Be(0);
        result.Should().ContainKey("TI_PDI_SixthByte").WhoseValue.Should().Be(0);
        result.Should().ContainKey("TI_PDI_SeventhByte").WhoseValue.Should().Be(0);
        result.Should().ContainKey("TI_PDI_EighthByte").WhoseValue.Should().Be(0);
        result.Should().ContainKey("TI_PDI_SecondBitHeader").WhoseValue.Should().Be(128);
    }

    [Fact]
    public void ShouldConvertProcessDataForRealDevice459267()
    {
        var data = Convert.FromBase64String("cHA=");
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load("TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml"));
        var pdResolver = new ProcessDataTypeResolver(device);
        var converter = new IoddConverter();

        var convertibleType = pdResolver.ResolveProcessDataIn()!;
        var result = (converter.ConvertFromIoLink(convertibleType, data) as List<(string, object)>)!.ToDictionary(x => x.Item1, y => y.Item2);

        result.Should().ContainKey("TN_PDI_BDC1").WhoseValue.Should().BeOfType<bool>().And.Be(false);
        result.Should().ContainKey("TN_PDI_PDV").WhoseValue.Should().Be(1799);
    }
}