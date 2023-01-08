namespace IOLinkNET.IODD.Structure.Datatypes;

public record StringT(string? Id, byte FixedLength, StringTEncoding Encoding) : SimpleDatatypeT(Id);

public static class StringTEncodingConstats
{
    public const string UTF8 = "UTF-8";

    public const string ASCII = "US-ASCII";
}

public enum StringTEncoding
{
    UTF8,
    ASCII
}