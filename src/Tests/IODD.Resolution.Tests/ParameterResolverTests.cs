usingIOLinkNET.IODD;

namespace IODD.Resolution.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        IODDParser parser = new();
        _ = parser.Parse(XElement.Load("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml"));
        var parameterResolver = new ParameterTypeResolver();
    }
}