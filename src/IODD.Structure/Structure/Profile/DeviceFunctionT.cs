using IODD.Structure.Structure.DeviceFunction;

using IOLinkNET.IODD.Structure.DataTypes;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IODD.Structure.Structure.Profile;

public record DeviceFunctionT(IEnumerable<DatatypeT> DatatypeCollection, IEnumerable<VariableT> VariableCollection, IEnumerable<ProcessDataT> ProcessDataCollection);