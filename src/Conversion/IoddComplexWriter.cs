using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

internal class IoddComplexWriter
{
    public static ReadOnlySpan<byte> Convert(ParsableComplexDataTypeDef complexTypeDef, object value)
        => complexTypeDef switch
        {
            ParsableRecord recordType => WriteRecordType(recordType, value),
            ParsableArray arrayTypeDef => WriteArrayT(arrayTypeDef, value),
            _ => throw new InvalidOperationException($"Type {complexTypeDef.GetType().Name} is not supported.")
        };
    private static ReadOnlySpan<byte> WriteArrayT(ParsableArray arrayTypeDef, object value) => throw new NotImplementedException();
    private static ReadOnlySpan<byte> WriteRecordType(ParsableRecord recordType, object value) => throw new NotImplementedException();
}