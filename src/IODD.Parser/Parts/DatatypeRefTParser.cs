using System.Xml.Linq;

using IODD.Parser.Helpers;

using IOLinkNET.IODD.Parser;
using IOLinkNET.IODD.Structure.Common;

namespace IODD.Parser.Parts;

internal class DatatypeRefTParser : IParserPart<DatatypeRefT>
{
    public bool CanParse(XName name)
        => name == IODDParserConstants.DatatypeRefName;

    public DatatypeRefT Parse(XElement element)
    {
        string datatypeId = element.ReadMandatoryAttribute("datatypeId");

        return new DatatypeRefT(datatypeId);
    }
}