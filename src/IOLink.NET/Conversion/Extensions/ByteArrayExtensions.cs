namespace Conversion.Extensions;

public static class ByteArrayExtensions
{
    /// <summary>
    /// If we have a negative integer, the resulting bytearray will be filled with 1s on the left, that may exceed the given bit length.
    /// This method will set the first (8 - bitLength % 8) bits to 0.
    /// Requires big-endian byte array. 
    public static byte[] PinNegativeIntToRequiredBitLength(this byte[] data, ushort bitLength)
    {
        if (bitLength % 8 == 0)
        {
            // In this case we have a full byte and don't need to pin anything
            return data;
        }
        var mask = (byte)(-1 << bitLength % 8);
        data[0] ^= mask;
        return data;
    }


    public static byte[] TruncateToBitLength(this byte[] data, ushort bitLength)
    {
        var requiredByteLength = bitLength / 8 + (bitLength % 8 != 0 ? 1 : 0);
        return data[(data.Length - requiredByteLength)..];
    }
}
