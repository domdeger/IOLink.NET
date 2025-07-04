using Conversion;
using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

public class IoddConverter : IIoddDataConverter
{
    public object Convert(ParsableDatatype datatypeDef, ReadOnlySpan<byte> data) =>
        datatypeDef switch
        {
            ParsableComplexDataTypeDef complexType => IoddComplexConverter.Convert(
                complexType,
                data
            ),
            ParsableSimpleDatatypeDef simpleType => IoddScalarReader.Convert(simpleType, data),
            _ => throw new NotImplementedException(),
        };

    public byte[] ConvertToBytes(object value, ParsableDatatype datatypeDef) =>
        datatypeDef switch
        {
            ParsableComplexDataTypeDef complexType => IoddComplexWriter.Write(complexType, value),
            ParsableSimpleDatatypeDef simpleType => IoddScalarWriter.Write(simpleType, value),
            _ => throw new NotImplementedException(),
        };
}
