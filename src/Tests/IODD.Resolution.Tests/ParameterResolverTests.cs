using System.Xml.Linq;
using FluentAssertions;

using IODD.Resolution.Model;

using IOLinkNET.IODD;

namespace IODD.Resolution.Tests;

public class UnitTest1
{
    [Fact]
    public void Can_Resolve_ScalarValue()
    {
        IODDParser parser = new();
        var device = parser.Parse(XElement.Load("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml")) 
            ?? throw new NullReferenceException();
        var parameterResolver = new ParameterTypeResolver(device);

        var param = parameterResolver.GetParameter(210);
        param.Should().NotBeNull();
        param.Should().BeOfType<ParsableRecord>();
    }
}