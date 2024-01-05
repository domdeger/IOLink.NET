using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.Conversion;

internal class IoddScalarWriter
{
    public static ReadOnlySpan<byte> Convert(ParsableSimpleDatatypeDef simpleTypeDef, object value)
        => simpleTypeDef switch
        {
            { Datatype: KindOfSimpleType.Boolean } => BitConverter.GetBytes((bool)value),
            { Datatype: KindOfSimpleType.Float } => WriteFloat((float)value),
            { Datatype: KindOfSimpleType.UInteger } => WriteUint((uint)value, simpleTypeDef.Length),
            { Datatype: KindOfSimpleType.Integer } => WriteInt((int)value, simpleTypeDef.Length),
            { Datatype: KindOfSimpleType.OctetString } => System.Convert.FromHexString((string)value),
            ParsableStringDef s => ConvertString(s, (string)value),
            _ => throw new NotImplementedException()
        };

    private static ReadOnlySpan<byte> WriteFloat(float value)
    {
        var result = new byte[4];
        BinaryPrimitives.WriteSingleBigEndian(result, value);
        return result;
    }

    private static ReadOnlySpan<byte> WriteUint(uint value, ushort length)
    {
        var result = new byte[length / 8 + (length % 8 != 0 ? 1 : 0)];

        if (length <= 8)
        {
            result[0] = (byte)value;
        }
        else if (length <= 16)
        {
            BinaryPrimitives.WriteUInt16BigEndian(result, (ushort)value);
        }
        else if (length <= 32)
        {
            BinaryPrimitives.WriteUInt32BigEndian(result, value);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "Length must be between 1 and 32.");
        }

        return result;
    }

    private static ReadOnlySpan<byte> WriteInt(int value, ushort length)
    {
        var result = new byte[length / 8 + (length % 8 != 0 ? 1 : 0)];

        if (length <= 8)
        {
            result[0] = (byte)value;
        }
        else if (length <= 16)
        {
            BinaryPrimitives.WriteInt16BigEndian(result, (short)value);
        }
        else if (length <= 32)
        {
            BinaryPrimitives.WriteInt32BigEndian(result, value);
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "Length must be between 1 and 32.");
        }

        return result;
    }

    private static ReadOnlySpan<byte> ConvertString(ParsableStringDef stringTypeDef, string data)
    {
        var result = new byte[stringTypeDef.Length / 8 + (stringTypeDef.Length % 8 != 0 ? 1 : 0)];
        var encoding = stringTypeDef.Encoding switch
        {
            StringTEncoding.ASCII => System.Text.Encoding.ASCII,
            StringTEncoding.UTF8 => System.Text.Encoding.UTF8,
            _ => throw new NotImplementedException()
        };
        var bytes = encoding.GetBytes(data);
        bytes.CopyTo(result.AsSpan()); // Explicitly specify the target span

        return result;
    }
}