
using System.Text;

using IOLinkNET.IODD.Resolution;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.Conversion;

internal class IoddScalarConverter
{
    public static object Convert(ParsableSimpleDatatypeDef typeDef, ReadOnlySpan<byte> data)
        => typeDef switch
        {
            { Datatype: KindOfSimpleType.Boolean } => BitConverter.ToBoolean(data),
            { Datatype: KindOfSimpleType.Float } => BitConverter.ToHalf(data),
            { Datatype: KindOfSimpleType.UInteger } => BitConverter.ToUInt64(data),
            { Datatype: KindOfSimpleType.Integer } => BitConverter.ToInt64(data),
            ParsableStringDef s => ConvertString(s, data),
            _ => throw new NotImplementedException()
        };

    private static string ConvertString(ParsableStringDef stringDef, ReadOnlySpan<byte> data)
        => stringDef.Encoding switch
        {
            StringTEncoding.ASCII => Encoding.ASCII.GetString(data),
            StringTEncoding.UTF8 => Encoding.UTF8.GetString(data),
            _ => throw new NotImplementedException()
        };
}