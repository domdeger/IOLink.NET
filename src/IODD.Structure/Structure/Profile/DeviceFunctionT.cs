using IOLinkNET.IODD.Structure.Structure.Menu;

using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Structure.Profile;

public record DeviceFunctionT(IEnumerable<DatatypeT> DatatypeCollection, IEnumerable<VariableT> VariableCollection, IEnumerable<ProcessDataT> ProcessDataCollection, UserInterfaceT UserInterface);