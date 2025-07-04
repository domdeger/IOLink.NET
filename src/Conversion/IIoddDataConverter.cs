using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

public interface IIoddDataConverter
{
    object Convert(ParsableDatatype datatypeDef, ReadOnlySpan<byte> data);
    byte[] ConvertToBytes(object value, ParsableDatatype datatypeDef);
}
