using System.Xml.Linq;

using IOLink.NET.IODD.Helpers;

using IOLink.NET.IODD.Parser;
using IOLink.NET.IODD.Structure.Common;

namespace IOLink.NET.IODD.Parts;

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
