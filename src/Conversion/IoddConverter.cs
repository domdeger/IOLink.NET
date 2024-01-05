using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

public class IoddConverter : IIoddDataConverter
{
    public object ConvertFromIoLink(ParsableDatatype datatypeDef, ReadOnlySpan<byte> data) => datatypeDef switch
    {
        ParsableComplexDataTypeDef complexType => IoddComplexReader.Convert(complexType, data),
        ParsableSimpleDatatypeDef simpleType => IoddScalarReader.Convert(simpleType, data),
        _ => throw new NotImplementedException()
    };

    public ReadOnlySpan<byte> ConvertToIoLink(ParsableDatatype datatypeDef, object value) => datatypeDef switch
    {
        ParsableComplexDataTypeDef complexType => IoddComplexWriter.Convert(complexType, value),
        ParsableSimpleDatatypeDef simpleType => IoddScalarWriter.Convert(simpleType, value),
        _ => throw new NotImplementedException()
    };
}
