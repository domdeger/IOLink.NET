using IOLink.NET.IODD.Structure.Datatypes;
using IOLink.NET.IODD.Structure.DeviceFunction;
using IOLink.NET.IODD.Structure.Interfaces.Menu;
using IOLink.NET.IODD.Structure.ProcessData;

namespace IOLink.NET.IODD.Structure.Interfaces.Profile;
public interface IDeviceFunctionT
{
    IEnumerable<DatatypeT> DatatypeCollection { get; }
    IEnumerable<VariableT> VariableCollection { get; }
    IEnumerable<ProcessDataT> ProcessDataCollection { get; }
    IUserInterfaceT UserInterface { get; }
}
