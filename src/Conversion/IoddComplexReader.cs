using System.Collections;

using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

internal static class IoddComplexReader
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
        var bits = new BitArray(data.ToArray().Reverse().ToArray());
        var arrayBitLength = arrayTypeDef.Length * arrayTypeDef.Type.Length;
        for (var i = 1; i <= arrayTypeDef.Length; i++)
        {
            var itemOffset = (ushort)(arrayBitLength - i * arrayTypeDef.Type.Length);
            var itemData = ReadWithPadding(bits, itemOffset, arrayTypeDef.Type.Length);
            result.Add(($"{arrayTypeDef.Name}_{i}", IoddScalarReader.Convert(arrayTypeDef.Type, itemData)));
        }

        return result;
    }

    private static IEnumerable<(string key, object value)> ConvertRecordType(ParsableRecord recordType, ReadOnlySpan<byte> data)
    {
        var result = new List<(string key, object value)>();
        var bits = new BitArray(data.ToArray().Reverse().ToArray());
        foreach (ParsableRecordItem? recordItemDef in recordType.Entries.OrderBy(x => x.BitOffset))
        {
            var translatedOffset = (ushort)(recordType.Length - recordItemDef.BitOffset);
            result.Add((recordItemDef.Name,
                IoddScalarReader.Convert(recordItemDef.Type,
                ReadWithPadding(bits, recordItemDef.BitOffset, recordItemDef.Type.Length).ToArray().Reverse().ToArray())));
        }

        return result;
    }

    private static ReadOnlySpan<byte> ReadWithPadding(BitArray bits, ushort offset, ushort length)
    {
        var result = new byte[length / 8 + (length % 8 != 0 ? 1 : 0)];

        for (var i = 0; i < length; i++)
        {
            var bit = bits[offset + i];
            result[i / 8] |= (byte)(bit ? 1 << (i % 8) : 0);
        }

        return result;
    }
}