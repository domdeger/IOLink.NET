using IOLink.NET.IODD.Resolution;

namespace IOLink.NET.Conversion;

public interface IIoddDataConverter
{
    object Convert(ParsableDatatype datatypeDef, ReadOnlySpan<byte> data);
    byte[] ConvertToBytes(object value, ParsableDatatype datatypeDef);
}
