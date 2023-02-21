
using System.Buffers.Binary;
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
            { Datatype: KindOfSimpleType.Float } => BinaryPrimitives.ReadSingleBigEndian(data),
            { Datatype: KindOfSimpleType.UInteger } => GetUint(data),
            { Datatype: KindOfSimpleType.Integer } => GetInt(data, typeDef.Length),
            ParsableStringDef s => ConvertString(s, data),
            _ => throw new NotImplementedException()
        };

    private static object GetUint(ReadOnlySpan<byte> data)
        => data.Length switch
        {
            1 => data[0],
            2 => BinaryPrimitives.ReadUInt16BigEndian(data),
            > 2 and <= 4 => BinaryPrimitives.ReadUInt32BigEndian(PadToIfNeeded(4, data)),
            > 4 and <= 8 => (object)BinaryPrimitives.ReadUInt64BigEndian(PadToIfNeeded(8, data)),
            _ => throw new ArgumentOutOfRangeException(nameof(data), "Data is too long to be converted into a long")
        };

    private static object GetInt(ReadOnlySpan<byte> data, ushort bitLength)
        => (object)data.Length switch
        {
            1 => BinaryPrimitives.ReadInt16BigEndian(PadToComplementaryIntIfNeeded(2, bitLength, data)),
            2 => BinaryPrimitives.ReadInt16BigEndian(PadToComplementaryIntIfNeeded(2, bitLength, data)),
            > 2 and <= 4 => BinaryPrimitives.ReadInt32BigEndian(PadToComplementaryIntIfNeeded(4, bitLength, data)),
            > 4 and <= 8 => (object)BinaryPrimitives.ReadInt64BigEndian(PadToComplementaryIntIfNeeded(8, bitLength, data)),
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
        static int TranslateToBitArrayIndex(int byteSize, int bitIndex)
        {
            int bitSize = byteSize * 8;
            int bitIndexInTargetByte = bitIndex % 8;

            if (byteSize == 1)
            {
                return bitIndex;
            }

            int result = bitSize - bitIndex - 8 + bitIndexInTargetByte;

            return result;
        }

        byte[] dataArray = data.ToArray();

        var bitRepresentation = new BitArray(dataArray);
        bool signBit = bitRepresentation[TranslateToBitArrayIndex(data.Length, bitLength - 1)];

        byte[] result = new byte[size];
        CopyToEnd(result, bitRepresentation, bitLength);

        if (signBit)
        {
            int payloadOffset = GetOffset(result.Length, bitLength);
            for (byte i = 0; i < payloadOffset; i++)
            {
                result[i] = BuildSignByte(8);
            }

            int signMaskLength = (size * 8) - (payloadOffset * 8) - bitLength;
            result[payloadOffset] = (byte)(result[payloadOffset] | BuildSignByte(signMaskLength));
        }

        return result.AsSpan();
    }

    private static byte BuildSignByte(int count)
    {
        byte result = 0xff;
        for (int i = count; i < 8; i++)
        {
            result <<= 1;
        }

        return result;
    }

    private static void CopyToEnd(byte[] result, BitArray bitRepresentation, int bitLength)
    {
        int offset = GetOffset(result.Length, bitLength);
        bitRepresentation.CopyTo(result, offset);
    }

    private static int GetOffset(int containerLength, int bitLength) => containerLength - ((bitLength / 8) + 1);

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