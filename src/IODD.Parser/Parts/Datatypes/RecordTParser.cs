using System.Xml.Linq;

using IOLinkNET.IODD.Helpers;
using IOLinkNET.IODD.Parts.Constants;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.Datatypes;

namespace IOLinkNET.IODD.Parts.Datatypes;

internal static class RecordTParser
{
    public static RecordT Parse(XElement elem, IParserPartLocator parserLocator)
    {
        string? id = elem.ReadOptionalAttribute("id");
        ushort bitLenght = elem.ReadMandatoryAttribute<ushort>("bitLength");

        _ = bool.TryParse(elem.ReadOptionalAttribute("subindexAccessSupported"), out bool subindexAccessSupported);

        return new RecordT(id, bitLenght, elem.Descendants(IODDDeviceFunctionNames.RecordItemName).Select(elem => ParseRecordItem(elem, parserLocator)), subindexAccessSupported);
    }

    private static RecordItemT ParseRecordItem(XElement elem, IParserPartLocator parserLocator)
    {
        byte subIndex = elem.ReadMandatoryAttribute<byte>("subindex");
        ushort bitOffset = elem.ReadMandatoryAttribute<ushort>("bitOffset");

        TextRefT name = parserLocator.ParseMandatory<TextRefT>(elem.Elements(IODDTextRefNames.Name).First());
        TextRefT? description = parserLocator.ParseOptional<TextRefT>(elem.Elements(IODDTextRefNames.DescriptionName).FirstOrDefault());
        DatatypeRefT? typeRef = parserLocator.ParseOptional<DatatypeRefT>(elem.Elements(IODDParserConstants.DatatypeRefName).FirstOrDefault());

        return new RecordItemT(subIndex, bitOffset, name, description, SimpleTypeParser.Parse(elem.Descendants(IODDParserConstants.SimpleDatatypeName).FirstOrDefault()), typeRef);
    }
}