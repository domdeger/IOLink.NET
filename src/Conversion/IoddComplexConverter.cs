using IOLinkNET.IODD.Resolution;

namespace IOLinkNET.Conversion;

internal static class IoddComplexConverter
{
    public static object Convert(ParsableComplexDataTypeDef complexTypeDef, ReadOnlySpan<byte> data)
        => complexTypeDef switch
        {
            ParsableRecord recordType => ConvertRecordType(recordType, data),
            // ToDo: ArrayT missing
            _ => throw new InvalidOperationException($"Type {complexTypeDef.GetType().Name} is not supported.")
        };

    private static IEnumerable<(string key, object value)> ConvertRecordType(ParsableRecord recordType, ReadOnlySpan<byte> data)
    {
        var result = new List<(string key, object value)>();

        foreach (var recordItemDef in recordType.Entries.OrderBy(x => x.BitOffset))
        {
            result.Add((recordItemDef.Name,
                IoddScalarConverter.Convert(recordItemDef.Type,
                ReadWithPadding(data, recordItemDef.BitOffset, recordItemDef.Type.Length))));
        }

        return result;
    }

    private static ReadOnlySpan<byte> ReadWithPadding(ReadOnlySpan<byte> data, ushort offset, ushort length)
    {
        var startByte = offset / 8;
        var endByte = startByte + (length / 8) + 1;

        var result = data[startByte..endByte];

        var startShiftNeeded = offset % 8 != 0;
        var endShiftNeeded = (length + offset) % 8 != 0;

        if (startShiftNeeded || endShiftNeeded)
        {
            var temp = result.ToArray();
            temp[0] = startShiftNeeded ? (byte)(temp[0] >> offset % 8) : temp[0];
            temp[^1] = endShiftNeeded ? (byte)(temp[^1] >> length % 8) : temp[^1];

            result = temp.AsSpan();
        }

        return result;
    }

}