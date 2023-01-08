using System.Xml.Linq;

namespace IOLinkNET.IODD.Tests;

public class ParserTest
{
    [Fact]
    public void ShouldParseIODDs()
    {
        IODDParser parser = new();
        _ = parser.Parse(XElement.Load("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml"));
    }
}