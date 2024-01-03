namespace Conversion.Extensions;

public static class ByteArrayExtensions
{
    public static byte[] ReverseIfNeeded(this byte[] data)
        => BitConverter.IsLittleEndian ? data.Reverse().ToArray() : data;


    public static byte[] ReverseIfNeededAndTake(this byte[] data, int count)
        => (BitConverter.IsLittleEndian ? data.Reverse() : data)
            .Skip(data.Length - count)
            .Take(count)
            .ToArray();

}