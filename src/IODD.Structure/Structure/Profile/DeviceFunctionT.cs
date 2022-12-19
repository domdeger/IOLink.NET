using IOLinkNET.IODD.Structure.DataTypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Structure.Profile;

public record DeviceFunctionT(IEnumerable<DatatypeT> DatatypeCollection, IEnumerable<VariableT> VariableCollection, IEnumerable<ProcessDataT> ProcessDataCollection);