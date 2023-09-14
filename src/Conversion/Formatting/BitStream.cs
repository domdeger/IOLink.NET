namespace IOLinkNET.Formatting;

public ref struct BitStream
{
    private ReadOnlySpan<byte> _data;
    public BitStream(ReadOnlySpan<byte> data)
    {
        _data = data;
    }

    public ReadOnlySpan<byte> GetBitSpan(int bitOffset, int bitLength)
    {
        var startByteOffset = bitOffset / 8;
        var endByteOffset = (bitOffset + bitLength - 1) / 8;

        var startBit = bitOffset % 8;
        var endBit = (bitOffset + bitLength - 1) % 8;

        // no bitshift required plain return
        if (startBit == 0 && endBit == 0)
        {
            return _data.Slice(startByteOffset, bitLength / 8);
        }

        if (startByteOffset == endByteOffset)
        {
            var mask = BuildMask(startBit, endBit);
            var extractedBits = (byte)(_data[startByteOffset] & mask);
            var result = (byte)(extractedBits >> startBit);
            return new byte[] { result };
        }

        var startMask = BuildMask(startBit, 8);
        var endMask = BuildMask(0, endBit);

        var startByte = (byte)(_data[startByteOffset] & startMask);
        var endByte = (byte)(_data[endByteOffset] & endMask);

        if (startByteOffset + 1 == endByteOffset)
        {
            return new byte[] { startByte, endByte };
        }

        return new byte[] { startByte }
                .Concat(_data.Slice(startByteOffset + 1, endByteOffset - startByteOffset - 1).ToArray())
                .Concat(new byte[] { endByte })
                .ToArray();
    }

    private byte BuildMask(int startBit, int endBit)
    {
        var mask = 0;
        for (var i = startBit; i <= endBit; i++)
        {
            mask |= 1 << i;
        }

        return (byte)mask;
    }
}