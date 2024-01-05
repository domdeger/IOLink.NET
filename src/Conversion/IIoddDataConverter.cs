using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

public interface IIoddDataConverter
{
    object ConvertFromIoLink(ParsableDatatype datatypeDef, ReadOnlySpan<byte> data);

    ReadOnlySpan<byte> ConvertToIoLink(ParsableDatatype datatypeDef, object value);
}