using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.Interfaces.Menu;
using IOLinkNET.IODD.Structure.Interfaces.Profile;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Structure.Profile;

public record DeviceFunctionT(IEnumerable<DatatypeT> DatatypeCollection, IEnumerable<VariableT> VariableCollection, IEnumerable<ProcessDataT> ProcessDataCollection, IUserInterfaceT UserInterface): IDeviceFunctionT;