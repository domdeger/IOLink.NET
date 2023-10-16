using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

public class IoddConverter : IIoddDataConverter
{
    public object Convert(ParsableDatatype datatypeDef, ReadOnlySpan<byte> data) => datatypeDef switch
    {
        ParsableComplexDataTypeDef complexType => IoddComplexConverter.Convert(complexType, data),
        ParsableSimpleDatatypeDef simpleType => IoddScalarConverter.Convert(simpleType, data),
        _ => throw new NotImplementedException()
    };
}