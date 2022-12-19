using IOLinkNET.IODD.Structure.Common;
using IOLinkNET.IODD.Structure.DataTypes;

namespace IOLinkNET.IODD.Structure.ProcessData;

public record ProcessDataItemT(DatatypeT? Datatype, DatatypeRefT? Ref, TextRefT Name, ushort BitLength);