using IOLinkNET.Integration;
using IOLinkNET.IODD.Structure;
using IOLinkNET.IODD.Structure.Interfaces.Menu;
using IOLinkNET.Visualization.IODDConversion;
using IOLinkNET.Visualization.Structure.Structure;

using static IOLinkNET.Integration.IODDPortReader;

namespace IOLinkNET.Visualization.Menu
{
    public class MenuDataReader
    {
        private readonly IODDPortReader _ioddPortReader;
        private IODevice? _device;
        private IODDUserInterfaceConverter? _iODDUserInterfaceConverter;

        public MenuDataReader(IODDPortReader ioddPortReader)
        {
            _ioddPortReader = ioddPortReader;
        }

        public async Task InitializeForPortAsync(byte port)
        {
            await _ioddPortReader.InitializeForPortAsync(port);
            _device = _ioddPortReader.Device;
            _iODDUserInterfaceConverter = new(_device, _ioddPortReader);
        }

        public IUserInterfaceT GetIODDRawMenuStructure()
        {
            return _device?.ProfileBody.DeviceFunction.UserInterface ?? throw new InvalidOperationException("MenuDataReader is not initialized");
        }

        public UIInterface GetReadableMenus()
        {
            if (_iODDUserInterfaceConverter == null)
            {
                throw new InvalidOperationException("MenuDataReader is not initialized");
            }

            return _iODDUserInterfaceConverter.Convert();
        }
    }
}
