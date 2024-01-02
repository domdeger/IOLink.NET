using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.DeviceFunction;
using IOLinkNET.IODD.Structure.Interfaces.Menu;
using IOLinkNET.IODD.Structure.ProcessData;

namespace IOLinkNET.IODD.Structure.Interfaces.Profile;
public interface IDeviceFunctionT
{
    IEnumerable<DatatypeT> DatatypeCollection { get; }
    IEnumerable<VariableT> VariableCollection { get; }
    IEnumerable<ProcessDataT> ProcessDataCollection { get; }
    IUserInterfaceT UserInterface { get; }
}
