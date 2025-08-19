using IOLink.NET.Core.Contracts;
using IOLink.NET.Integration;
using IOLink.NET.IODD.Structure;
using IOLink.NET.IODD.Structure.Interfaces.Menu;
using IOLink.NET.Visualization.IODDConversion;
using IOLink.NET.Visualization.Structure.Structure;

namespace IOLink.NET.Visualization.Menu
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
            return _device?.ProfileBody.DeviceFunction.UserInterface
                ?? throw new InvalidOperationException("MenuDataReader is not initialized");
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
