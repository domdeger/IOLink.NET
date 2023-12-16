using System.Xml.Linq;

namespace IOLinkNET.IODD.Tests;

public class ParserTest
{
    [Theory]
    [InlineData("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml", 888, 328205)]
    [InlineData("TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml", 888, 459267)]
    [InlineData("TestData/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml", 888, 393780)]
    [InlineData("TestData/STEGO-SmartSensor-CSS014-08-20190726-IODD1.1.xml", 1222, 18)]
    public void ShouldParseIODDs(string path, ushort expectedVendorId, uint expectedDeviceId)
    {
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load(path));

        device.Should().NotBeNull();
        device.ProfileBody.DeviceIdentity.VendorId.Should().Be(expectedVendorId);
        device.ProfileBody.DeviceIdentity.DeviceId.Should().Be(expectedDeviceId);
    }
}