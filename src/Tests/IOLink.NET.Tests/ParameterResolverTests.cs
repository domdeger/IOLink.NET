using System.Xml.Linq;
using IOLink.NET.IODD;
using IOLink.NET.IODD.Resolution;
using IOLink.NET.IODD.Structure;

namespace IOLink.NET.Tests;

public class ParameterResolverTests
{
    readonly IODevice _iodd;

    public ParameterResolverTests()
    {
        IODDParser parser = new();
        _iodd =
            parser.Parse(
                XElement.Load("TestData/Balluff-BNI_IOL-727-S51-P012-20220211-IODD1.1.xml")
            ) ?? throw new NullReferenceException();
    }

    [Fact]
    public void CanResolveRecordType()
    {
        var parameterResolver = new ParameterTypeResolver(_iodd);

        var param = parameterResolver.GetParameter(210);
        param.ShouldNotBeNull();
        param.ShouldBeOfType<ParsableRecord>();
        var recordParam = param as ParsableRecord;

        recordParam!.Name.ShouldBe("V_Inversion_Record");
        recordParam!.Entries?.ShouldNotBeEmpty();
        recordParam!
            .Entries.ElementAt(0)
            .ShouldBe(
                new(
                    new ParsableSimpleDatatypeDef(
                        "TI_VAR_Inversion_P0P4",
                        KindOfSimpleType.Boolean,
                        1
                    ),
                    "TI_VAR_Inversion_P0P4",
                    0,
                    1
                )
            );
        recordParam!
            .Entries.Last()
            .ShouldBe(
                new ParsableRecordItem(
                    new ParsableSimpleDatatypeDef(
                        "TI_VAR_Inversion_P3P2",
                        KindOfSimpleType.Boolean,
                        1
                    ),
                    "TI_VAR_Inversion_P3P2",
                    7,
                    8
                )
            );
    }

    [Fact]
    public void CanResolveScalarViaSubindex()
    {
        var parameterResolver = new ParameterTypeResolver(_iodd);

        var param = parameterResolver.GetParameter(210, 1);
        param.ShouldNotBeNull();
        param.ShouldBeOfType<ParsableSimpleDatatypeDef>();

        var simpleDatatype = param as ParsableSimpleDatatypeDef;
        simpleDatatype!.Datatype.ShouldBe(KindOfSimpleType.Boolean);
        simpleDatatype!.Name.ShouldBe("V_Inversion_Record_1");
    }

    [Fact]
    public void CanResolveScalar()
    {
        var parameterResolver = new ParameterTypeResolver(_iodd);

        var param = parameterResolver.GetParameter(112);
        param.ShouldNotBeNull();
        param.ShouldBeOfType<ParsableSimpleDatatypeDef>();

        var simpleDatatype = param as ParsableSimpleDatatypeDef;
        simpleDatatype!.Datatype.ShouldBe(KindOfSimpleType.UInteger);
        simpleDatatype!.Name.ShouldBe("V_Diag_Level_Config");
    }

    [Fact]
    public void CanResolveArray()
    {
        var parameterResolver = new ParameterTypeResolver(_iodd);

        var param = parameterResolver.GetParameter(113);
        param.ShouldNotBeNull();
        param.ShouldBeOfType<ParsableArray>();

        var arrayParam =
            param as ParsableArray
            ?? throw new InvalidOperationException("Did not receive an array.");
        arrayParam.Name.ShouldBe("V_EventCodeSupp");
        arrayParam.Length.ShouldBe((ushort)5);
        arrayParam.Type.ShouldBeOfType<ParsableSimpleDatatypeDef>();
        arrayParam.Type.Datatype.ShouldBe(KindOfSimpleType.UInteger);
        arrayParam!.Type.ShouldBeOfType<ParsableSimpleDatatypeDef>();
    }
}
