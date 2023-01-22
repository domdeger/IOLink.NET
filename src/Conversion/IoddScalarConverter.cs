
using System.Text;

using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.Conversion;

internal class IoddScalarConverter
{
    public static object Convert(ParsableSimpleDatatypeDef typeDef, ReadOnlySpan<byte> data)
        => typeDef switch
        {
            { Datatype: KindOfSimpleType.Boolean } => BitConverter.ToBoolean(data),
            { Datatype: KindOfSimpleType.Float } => BitConverter.ToHalf(data),
            { Datatype: KindOfSimpleType.UInteger } => GetUint(data),
            { Datatype: KindOfSimpleType.Integer } => BitConverter.ToInt64(data),
            ParsableStringDef s => ConvertString(s, data),
            _ => throw new NotImplementedException()
        };

    private static ulong GetUint(ReadOnlySpan<byte> data)
        => data.Length switch
        {
            1 => data[0],
            2 => BitConverter.ToUInt16(data),
            > 2 and <= 4 => BitConverter.ToUInt32(PadToIfNeeded(4, data)),
            > 4 => BitConverter.ToUInt64(PadToIfNeeded(8, data)),
            _ => throw new ArgumentOutOfRangeException(nameof(data), "Data is too long to be converted into a long")
        };

    private static ReadOnlySpan<byte> PadToIfNeeded(byte size, ReadOnlySpan<byte> data)
        => data switch
        {
            _ when data.Length == size => data,
            _ when data.Length < size => Enumerable.Range(0, size - data.Length).Select(_ => (byte)0).Concat(data.ToArray()).ToArray().AsSpan(),
            _ => throw new InvalidOperationException("Desired span width is smaller than the actual size of the input.")
        };

    private static string ConvertString(ParsableStringDef stringDef, ReadOnlySpan<byte> data)
        => stringDef.Encoding switch
        {
            StringTEncoding.ASCII => Encoding.ASCII.GetString(data),
            StringTEncoding.UTF8 => Encoding.UTF8.GetString(data),
            _ => throw new NotImplementedException($"Encoding {stringDef.Encoding} is not supported.")
        };
}