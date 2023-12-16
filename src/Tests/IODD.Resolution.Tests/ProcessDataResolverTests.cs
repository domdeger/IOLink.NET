using System.Xml.Linq;

using IOLinkNET.IODD;
using IOLinkNET.IODD.Resolution;

public class ProcessDataResolverTests
{
    [Theory]
    [InlineData("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml", false, null)]
    public void CanResolveProcessData(string iodd, bool hasCondition, int? condition)
    {
        var parser = new IODDParser();
        var device = parser.Parse(XElement.Load(iodd));
        var pdResolver = new ProcessDataTypeResolver(device);

        pdResolver.HasCondition().Should().Be(hasCondition);

        var parsablePdIn = pdResolver.ResolveProcessDataIn(condition);
        parsablePdIn.Should().NotBeNull();
        parsablePdIn.Should().BeOfType<ParsableRecord>();
    }
}