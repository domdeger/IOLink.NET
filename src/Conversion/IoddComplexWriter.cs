using System.Collections;
using IOLinkNET.IODD.Resolution;

namespace Conversion;

internal static class IoddComplexWriter
{
    public static byte[] Write(ParsableComplexDataTypeDef complexTypeDef, object value) =>
        complexTypeDef switch
        {
            ParsableRecord recordType => WriteRecordType(recordType, value),
            ParsableArray arrayTypeDef => WriteArrayType(arrayTypeDef, value),
            _ => throw new InvalidOperationException(
                $"Type {complexTypeDef.GetType().Name} is not supported."
            ),
        };

    private static byte[] WriteArrayType(ParsableArray arrayTypeDef, object value)
    {
        if (value is not IEnumerable<object> enumerable)
        {
            throw new ArgumentException(
                "Value must be an enumerable for array types",
                nameof(value)
            );
        }

        var items = enumerable.ToList();
        if (items.Count != arrayTypeDef.Length)
        {
            throw new ArgumentException(
                $"Array length mismatch. Expected {arrayTypeDef.Length}, got {items.Count}",
                nameof(value)
            );
        }

        var totalBitLength = arrayTypeDef.Length * arrayTypeDef.Type.Length;
        var bits = new BitArray(totalBitLength);

        for (var i = 0; i < arrayTypeDef.Length; i++)
        {
            var itemBytes = IoddScalarWriter.Write(arrayTypeDef.Type, items[i]);
            var itemBits = new BitArray(itemBytes);
            var itemOffset = i * arrayTypeDef.Type.Length;

            for (var j = 0; j < arrayTypeDef.Type.Length && j < itemBits.Length; j++)
            {
                bits[itemOffset + j] = itemBits[j];
            }
        }

        return ConvertBitArrayToBytes(bits);
    }

    private static byte[] WriteRecordType(ParsableRecord recordType, object value)
    {
        if (value is not IEnumerable<(string key, object value)> keyValuePairs)
        {
            throw new ArgumentException(
                "Value must be an enumerable of key-value pairs for record types",
                nameof(value)
            );
        }

        var pairs = keyValuePairs.ToDictionary(kvp => kvp.key, kvp => kvp.value);
        var bits = new BitArray(recordType.Length);

        foreach (var recordItem in recordType.Entries)
        {
            if (!pairs.TryGetValue(recordItem.Name, out var itemValue))
            {
                throw new ArgumentException(
                    $"Missing value for record item '{recordItem.Name}'",
                    nameof(value)
                );
            }

            var itemBytes = IoddScalarWriter.Write(recordItem.Type, itemValue);
            var itemBits = new BitArray(itemBytes);

            for (var i = 0; i < recordItem.Type.Length && i < itemBits.Length; i++)
            {
                bits[recordItem.BitOffset + i] = itemBits[i];
            }
        }

        return ConvertBitArrayToBytes(bits);
    }

    private static byte[] ConvertBitArrayToBytes(BitArray bits)
    {
        var bytes = new byte[(bits.Length + 7) / 8];
        bits.CopyTo(bytes, 0);
        return bytes.Reverse().ToArray();
    }
}
