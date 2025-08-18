using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.DeviceFunction;
using IOLink.NET.IODD.Structure.Interfaces.Menu;
using IOLink.NET.IODD.Structure.Interfaces.Profile;
using IOLink.NET.IODD.Structure.ProcessData;

namespace IOLink.NET.IODD.Structure.Profile;

public record DeviceFunctionT(IEnumerable<DatatypeT> DatatypeCollection, IEnumerable<VariableT> VariableCollection, IEnumerable<ProcessDataT> ProcessDataCollection, IUserInterfaceT UserInterface): IDeviceFunctionT;
