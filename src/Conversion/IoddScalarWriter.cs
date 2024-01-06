using System.Buffers.Binary;
using System.Numerics;
using System.Text;

using Conversion.Extensions;

using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Structure.Datatypes;

namespace Conversion;

public class IoddScalarWriter
{
    public static byte[] Write(ParsableSimpleDatatypeDef typeDef, object value)
        => typeDef switch
        {
            { Datatype: KindOfSimpleType.Boolean } => BitConverter.GetBytes((bool)value),
            { Datatype: KindOfSimpleType.Float } => WriteFloat(value),
            { Datatype: KindOfSimpleType.UInteger } => WriteInt(value, typeDef.Length),
            { Datatype: KindOfSimpleType.Integer } => WriteInt(value, typeDef.Length),
            { Datatype: KindOfSimpleType.OctetString } => Convert.FromHexString((string)value),
            ParsableStringDef s => WriteString(s, (string)value),
            _ => throw new NotImplementedException()
        };

    private static byte[] WriteInt(object value, ushort bitLength)
        => bitLength switch
        {
            <= 2 => throw new ArgumentOutOfRangeException(nameof(bitLength), bitLength, "Invalid bitLength for (U)Int -> byte[] write"),
            <= 16 => WriteInt(value, bitLength, Convert.ToInt16),
            <= 32 => WriteInt(value, bitLength, Convert.ToInt32),
            <= 64 => WriteInt(value, bitLength, Convert.ToInt64),
            _ => throw new ArgumentOutOfRangeException(nameof(bitLength), bitLength, "Invalid bitLength for (U)Int -> byte[] write")
        };

    private static byte[] WriteString(ParsableStringDef stringDef, string value)
    => stringDef.Encoding switch
    {
        StringTEncoding.ASCII => Encoding.ASCII.GetBytes(value),
        StringTEncoding.UTF8 => Encoding.UTF8.GetBytes(value),
        _ => throw new NotImplementedException($"Encoding {stringDef.Encoding} is not supported.")
    };

    private static byte[] WriteFloat(object value)
    {
        var bytes = new byte[4];
        BinaryPrimitives.WriteSingleBigEndian(bytes, (float)value);
        return bytes;
    }

    private static byte[] WriteInt<R>(object value, ushort bitLength, Func<object, R> conversionFunc) where R : IBinaryInteger<R>
    {
        R val = conversionFunc(value);
        byte[] bytes = new byte[val.GetByteCount()];
        val.WriteBigEndian(bytes);

        var requiredByteLength = bitLength / 8 + (bitLength % 8 != 0 ? 1 : 0);

        var limitedBytes = bytes.Skip(bytes.Length - requiredByteLength).ToArray();

        return R.IsNegative(val) ? limitedBytes.PinNegativeIntToRequiredBitLength(bitLength) : limitedBytes;
    }
}