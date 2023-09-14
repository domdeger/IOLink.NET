using System.Collections;
using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

internal static class IoddComplexConverter
{
    public static object Convert(ParsableComplexDataTypeDef complexTypeDef, ReadOnlySpan<byte> data)
        => complexTypeDef switch
        {
            ParsableRecord recordType => ConvertRecordType(recordType, data),
            ParsableArray arrayTypeDef => ConvertArrayT(arrayTypeDef, data),
            _ => throw new InvalidOperationException($"Type {complexTypeDef.GetType().Name} is not supported.")
        };

    private static IEnumerable<(string key, object value)> ConvertArrayT(ParsableArray arrayTypeDef, ReadOnlySpan<byte> data)
    {
        var result = new List<(string key, object value)>();

        for (var i = 0; i < arrayTypeDef.Length; i++)
        {
            var itemOffset = (ushort)(i * arrayTypeDef.Type.Length);
            var itemData = ReadWithPadding(data, itemOffset, arrayTypeDef.Type.Length);
            result.Add(($"{arrayTypeDef.Name}_{i}", IoddScalarConverter.Convert(arrayTypeDef.Type, itemData)));
        }

        return result;
    }

    private static IEnumerable<(string key, object value)> ConvertRecordType(ParsableRecord recordType, ReadOnlySpan<byte> data)
    {
        var result = new List<(string key, object value)>();

        foreach (ParsableRecordItem? recordItemDef in recordType.Entries.OrderBy(x => x.BitOffset))
        {
            result.Add((recordItemDef.Name,
                IoddScalarConverter.Convert(recordItemDef.Type,
                ReadWithPadding(data, recordItemDef.BitOffset, recordItemDef.Type.Length))));
        }

        return result;
    }

    private static ReadOnlySpan<byte> ReadWithPadding(ReadOnlySpan<byte> data, ushort offset, ushort length)
    {
        var bits = new BitArray(data.ToArray());
        var result = new byte[length / 8 + 1];

        for (var i = 0; i < length; i++)
        {
            var bit = bits[offset + i];
            result[i / 8] |= (byte)(bit ? 1 << (i % 8) : 0);
        }

        return result;
    }

}