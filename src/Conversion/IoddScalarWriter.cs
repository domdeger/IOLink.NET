using System.Buffers.Binary;
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
            { Datatype: KindOfSimpleType.Float } => BitConverter.GetBytes((float)value),
            { Datatype: KindOfSimpleType.UInteger } => WriteUint(value, typeDef.Length),
            { Datatype: KindOfSimpleType.Integer } => WriteInt(value, typeDef.Length),
            { Datatype: KindOfSimpleType.OctetString } => Convert.FromHexString((string)value).ToArray(),
            ParsableStringDef s => WriteString(s, (string)value),
            _ => throw new NotImplementedException()
        };

    private static byte[] WriteUint(object value, ushort bitLength)
        => bitLength switch
        {
            >= 2 and <= 8 => BitConverter
                .GetBytes(Convert.ToUInt16(value))
                .ReverseIfNeeded()
                .Take(1)
                .ToArray(),
            <= 16 => BitConverter.GetBytes(Convert.ToUInt16(value)).ReverseIfNeeded(),
            <= 24 => BitConverter.GetBytes(Convert.ToUInt32(value)).ReverseIfNeededAndTake(3),
            <= 32 => BitConverter.GetBytes(Convert.ToUInt32(value)).ReverseIfNeeded(),
            <= 40 => BitConverter.GetBytes(Convert.ToUInt64(value)).ReverseIfNeededAndTake(5),
            <= 48 => BitConverter.GetBytes(Convert.ToUInt64(value)).ReverseIfNeededAndTake(6),
            <= 56 => BitConverter.GetBytes(Convert.ToUInt64(value)).ReverseIfNeededAndTake(7),
            <= 64 => BitConverter.GetBytes(Convert.ToUInt64(value)).ReverseIfNeeded(),
            _ => throw new ArgumentOutOfRangeException(nameof(bitLength), bitLength, "Invalid bitLength for Uint -> byte[] write")
        };

    private static byte[] WriteInt(object value, ushort bitLength)
        => bitLength switch
        {
            >= 2 and <= 8 => [WriteInt8(value, bitLength)],
            <= 16 => BitConverter.GetBytes(Convert.ToInt16(value)).ReverseIfNeeded(),
            <= 24 => BitConverter.GetBytes(Convert.ToInt32(value)).ReverseIfNeededAndTake(3),
            <= 32 => BitConverter.GetBytes(Convert.ToInt32(value)).ReverseIfNeeded(),
            <= 40 => BitConverter.GetBytes(Convert.ToInt64(value)).ReverseIfNeededAndTake(5),
            <= 48 => BitConverter.GetBytes(Convert.ToInt64(value)).ReverseIfNeededAndTake(6),
            <= 56 => BitConverter.GetBytes(Convert.ToInt64(value)).ReverseIfNeededAndTake(7),
            <= 64 => BitConverter.GetBytes(Convert.ToInt64(value)).ReverseIfNeeded(),
            _ => throw new ArgumentOutOfRangeException(nameof(bitLength), bitLength, "Invalid bitLength for Int -> byte[] write")
        };

    private static byte[] WriteString(ParsableStringDef stringDef, string value)
    => stringDef.Encoding switch
    {
        StringTEncoding.ASCII => Encoding.ASCII.GetBytes(value),
        StringTEncoding.UTF8 => Encoding.UTF8.GetBytes(value),
        _ => throw new NotImplementedException($"Encoding {stringDef.Encoding} is not supported.")
    };

    private static byte WriteInt8(object value, ushort bitLength)
    {
        short val = Convert.ToInt16(value);
        byte convertedByte = BitConverter.GetBytes(val)[BitConverter.IsLittleEndian ? 0 : 1];
        if (val >= 0)
        {
            return convertedByte;
        }
        else
        {
            // We start with 0b1111_1111 and add 0s on the right, equivalent to bit length.
            // e.g. 0b1111_1111 << 4 = 0b1111_0000
            // This will get XORed with the converted byte, which will set the first (8 - bitLength) bits to 0.
            var mask = (byte)(-1 << bitLength);
            return (byte)(convertedByte ^ mask);
        }
    }


}