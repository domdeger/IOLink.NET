
using System.Collections;
using System.Text;

using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.Conversion;

public class IoddScalarConverter
{
    public static object Convert(ParsableSimpleDatatypeDef typeDef, ReadOnlySpan<byte> data)
        => typeDef switch
        {
            { Datatype: KindOfSimpleType.Boolean } => BitConverter.ToBoolean(data),
            { Datatype: KindOfSimpleType.Float } => BitConverter.ToHalf(data),
            { Datatype: KindOfSimpleType.UInteger } => GetUint(data),
            { Datatype: KindOfSimpleType.Integer } => GetInt(data, typeDef.Length),
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

    private static long GetInt(ReadOnlySpan<byte> data, ushort bitLength)
        => data.Length switch
        {
            1 => BitConverter.ToInt16(PadToComplementaryIntIfNeeded(2, bitLength, data)),
            2 => BitConverter.ToInt16(PadToComplementaryIntIfNeeded(2, bitLength, data)),
            > 2 and <= 4 => BitConverter.ToInt32(PadToComplementaryIntIfNeeded(4, bitLength, data)),
            > 4 => BitConverter.ToInt64(PadToComplementaryIntIfNeeded(8, bitLength, data)),
            _ => throw new ArgumentOutOfRangeException(nameof(data), "Data is too long to be converted into a long")
        };

    private static ReadOnlySpan<byte> PadToComplementaryIntIfNeeded(byte size, ushort bitLength, ReadOnlySpan<byte> data)
        => data switch
        {
            _ when bitLength % 8 == 0 && bitLength / 8 == size => data,
            _ when data.Length <= size => PadToComplementaryInt(size, bitLength, data),
            _ => throw new InvalidOperationException("Desired span width is smaller than the actual size of the input.")
        };

    private static ReadOnlySpan<byte> PadToComplementaryInt(byte size, ushort bitLength, ReadOnlySpan<byte> data)
    {
        byte[] dataArray = data.ToArray();

        static int ConvertToBitArrayIndex(int bitIndex, int byteLength)
        {
            int totalBitLength = byteLength * 8;
            int desiredBit = totalBitLength - bitIndex;

            int result = 7 - desiredBit;

            return result;
        }

        Console.Write(ConvertToBitArrayIndex(1, 0));

        var bitRepresentation = new BitArray(dataArray);
        bool signBit = bitRepresentation[^(bitLength + 1)];

        var result = new BitArray(size * 8, signBit);

        for (int i = 0; i < bitLength; i++)
        {
            result[i] = bitRepresentation[i];
        }

        byte[] resultBytes = new byte[size];
        result.CopyTo(resultBytes, 0);

        return resultBytes.AsSpan();
    }


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