using System.Collections;
using IOLink.NET.IODD.Resolution;

namespace IOLink.NET.Conversion;

public static class IoddComplexWriter
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

            // For bit-packed records, handle small bit lengths directly
            var itemBits = ConvertValueToBits(recordItem.Type, itemValue);

            for (var i = 0; i < recordItem.Type.Length && i < itemBits.Length; i++)
            {
                bits[recordItem.BitOffset + i] = itemBits[i];
            }
        }

        return ConvertBitArrayToBytes(bits);
    }

    private static BitArray ConvertValueToBits(ParsableSimpleDatatypeDef typeDef, object value)
    {
        // For very small bit lengths, handle the conversion directly
        if (
            typeDef.Length <= 8
            && (
                typeDef.Datatype == KindOfSimpleType.UInteger
                || typeDef.Datatype == KindOfSimpleType.Integer
            )
        )
        {
            var numericValue = Convert.ToUInt64(value);
            var bits = new BitArray(typeDef.Length);

            for (var i = 0; i < typeDef.Length; i++)
            {
                bits[i] = (numericValue & (1UL << i)) != 0;
            }

            return bits;
        }

        // For larger or other types, use the scalar writer
        var bytes = IoddScalarWriter.Write(typeDef, value);

        // For multi-field records, we need to account for the reader's field-level reversal
        // The reader will reverse the bytes of each field individually, so we pre-reverse them
        var reversedBytes = bytes.Reverse().ToArray();
        return new BitArray(reversedBytes);
    }

    private static byte[] ConvertBitArrayToBytes(BitArray bits)
    {
        var bytes = new byte[(bits.Length + 7) / 8];

        // Match the reader's bit packing logic
        for (var i = 0; i < bits.Length; i++)
        {
            var bit = bits[i];
            bytes[i / 8] |= (byte)(bit ? 1 << (i % 8) : 0);
        }

        // Reverse to compensate for the reader's reversal
        return bytes.Reverse().ToArray();
    }
}
