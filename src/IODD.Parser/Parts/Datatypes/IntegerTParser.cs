using System.Xml.Linq;

using IODD.Parser.Helpers;

using IOLinkNET.IODD.Structure.DataTypes;

namespace IODD.Parser.Parts.Datatypes;

internal static class IntegerTParser
{
    public static IntegerT ParseInt(XElement elem)
    {
        string id = elem.ReadMandatoryAttribute("id");
        ushort bitLength = elem.ReadMandatoryAttribute<ushort>("bitLength");

        return new IntegerT(id, bitLength, Enumerable.Empty<SingleValueT<int>>(), Enumerable.Empty<ValueRangeT<int>>());
    }

    public static UIntegerT ParseUInt(XElement elem)
    {
        string id = elem.ReadMandatoryAttribute("id");
        ushort bitLength = elem.ReadMandatoryAttribute<ushort>("bitLength");

        return new UIntegerT(id, bitLength, Enumerable.Empty<SingleValueT<uint>>(), Enumerable.Empty<ValueRangeT<uint>>());
    }
}