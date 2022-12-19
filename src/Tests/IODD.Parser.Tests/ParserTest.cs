using System.Xml.Linq;
using IOLinkNET.IODD.Parser;

namespace IODD.Parser.Tests;

public class UnitTest1
{
    [Fact]
    public void Should_Parse_IODDs()
    {
        IODDParser parser = new IODDParser();
        parser.Parse(XElement.Load("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml"));
    }
}