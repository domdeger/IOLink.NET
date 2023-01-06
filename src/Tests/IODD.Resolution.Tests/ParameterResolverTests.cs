using System.Xml.Linq;

using FluentAssertions;

using IODD.Resolution.Model;

using IOLinkNET.IODD;
using IOLinkNET.IODD.Structure;

namespace IODD.Resolution.Tests;

public class ParameterResolverTests
{
    readonly IODevice _iodd;
    public ParameterResolverTests()
    {
        IODDParser parser = new();
        _iodd = parser.Parse(XElement.Load("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml"))
            ?? throw new NullReferenceException();
    }


    [Fact]
    public void Can_Resolve_Record_Type()
    {
        var parameterResolver = new ParameterTypeResolver(_iodd);

        var param = parameterResolver.GetParameter(210);
        param.Should().NotBeNull();
        param.Should().BeOfType<ParsableRecord>();
        var recordParam = param as ParsableRecord;

        recordParam?.Entries?.Should().NotBeEmpty();
        recordParam!.Entries.Should().HaveElementAt(0, new(new ParsableSimpleDatatypeDef("DT_Inversion", KindOfSimpleType.Boolean, 1), "TI_VAR_Inversion_P0P4", 0, 1));
        recordParam!.Entries.Should().EndWith(new ParsableRecordItem(new ParsableSimpleDatatypeDef("DT_Inversion", KindOfSimpleType.Boolean, 1), "TI_VAR_Inversion_P0P4", 7, 8));
    }

    [Fact]
    public void Can_Resolve_Scalar_Via_Subindex()
    {
        var parameterResolver = new ParameterTypeResolver(_iodd);

        var param = parameterResolver.GetParameter(210, 1);
        param.Should().NotBeNull().And.BeOfType<ParsableSimpleDatatypeDef>();
    }
}