using System.Xml.Linq;

using IOLinkNET.IODD.Standard.Structure;
using IOLinkNET.IODD.Structure;

namespace IOLinkNET.IODD.Tests;

public class ParserTest
{
    [Theory]
    [InlineData("TestData/ifm-0002DD-20230324-IODD1.1.xml", 310, 733)]
    [InlineData("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml", 888, 328205)]
    [InlineData("TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml", 888, 459267)]
    [InlineData("TestData/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml", 888, 393780)]
    [InlineData("TestData/STEGO-SmartSensor-CSS014-08-20190726-IODD1.1.xml", 1222, 18)]
    public void ShouldParseIODDDeviceIdentity(string path, ushort expectedVendorId, uint expectedDeviceId)
    {
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load(path));

        device.Should().NotBeNull();
        device.ProfileBody.DeviceIdentity.VendorId.Should().Be(expectedVendorId);
        device.ProfileBody.DeviceIdentity.DeviceId.Should().Be(expectedDeviceId);
    }

    [Theory]
    [InlineData("TestData/ifm-0002DD-20230324-IODD1.1.xml", true, 46)]
    [InlineData("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml", true, 84)]
    [InlineData("TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml", true, 8)]
    [InlineData("TestData/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml", true, 79)]
    [InlineData("TestData/STEGO-SmartSensor-CSS014-08-20190726-IODD1.1.xml", true, 26)]
    public void ShouldParseIODDDeviceFunctionUserInterface(string path, bool hasMenus, int menuCollectionCount)
    {
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load(path));

        if (hasMenus)
        {
            device.ProfileBody.DeviceFunction.UserInterface.Should().NotBeNull();
            device.ProfileBody.DeviceFunction.UserInterface.MenuCollection?.Count().Should().Be(menuCollectionCount);
        }
        else
        {
            device.ProfileBody.DeviceFunction.UserInterface.Should().BeNull();
        }
    }

    [Theory]
    [InlineData("TestData/ifm-0002DD-20230324-IODD1.1.xml", 161)]
    [InlineData("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml", 436)]
    [InlineData("TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml", 61)]
    [InlineData("TestData/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml", 268)]
    [InlineData("TestData/STEGO-SmartSensor-CSS014-08-20190726-IODD1.1.xml", 152)]
    public void ShouldParseIODDExternalTextCollection(string path, int externalTextCollectionTextDefinitionCount)
    {
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load(path));

        device.ExternalTextCollection.Should().NotBeNull();
        device.ExternalTextCollection.TextDefinitions.Count().Should().Be(externalTextCollectionTextDefinitionCount);
    }

    [Theory]
    [InlineData("TestData/ifm-0002DD-20230324-IODD1.1.xml")]
    [InlineData("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml")]
    [InlineData("TestData/Balluff-BCS_R08RRE-PIM80C-20150206-IODD1.1.xml")]
    [InlineData("TestData/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml")]
    [InlineData("TestData/STEGO-SmartSensor-CSS014-08-20190726-IODD1.1.xml")]
    public void ShouldParseStandardDefinitions(string path)
    {
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load(path));

        device.StandardDatatypeCollection.Should().NotBeNull();
        device.StandardDatatypeCollection.Should().HaveCount(2);
    }
}