using IOLinkNET.Integration;
using IOLinkNET.IODD.Structure.Structure.Menu;
using IOLinkNET.Visualization.IODDConversion;
using IOLinkNET.Visualization.Structure.Structure;

using static IOLinkNET.Integration.IODDPortReader;

namespace IOLinkNET.Visualization.Menu
{
    public class MenuDataReader
    {
        private readonly IODDPortReader _ioddPortReader;
        private PortReaderInitilizationResult? _initilizationState;
        private IODDUserInterfaceConverter? _iODDUserInterfaceConverter;

        public MenuDataReader(IODDPortReader ioddPortReader)
        {
            _ioddPortReader = ioddPortReader;
        }

        public async Task InitializeForPortAsync(byte port)
        {
            await _ioddPortReader.InitializeForPortAsync(port);
            _initilizationState = _ioddPortReader.InitilizationState;
            _iODDUserInterfaceConverter = new(GetIODDRawMenuStructure());
        }

        public UserInterfaceT GetIODDRawMenuStructure()
        {
            return _initilizationState?.DeviceDefinition.ProfileBody.DeviceFunction.UserInterface ?? throw new InvalidOperationException("PortReader is not initialized");
        }

        public UIInterface GetUIInterface()
        {
            if(_iODDUserInterfaceConverter == null)
            {
                throw new InvalidOperationException("MenuDataReader is not initialized");
            }

            return _iODDUserInterfaceConverter.Convert();
        }
    }
}
