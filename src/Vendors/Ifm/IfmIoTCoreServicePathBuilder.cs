namespace IOLinkNET.Vendors.Ifm;

internal static class IfmIoTCoreServicePathBuilder
{
    public static string PortDeviceStatusPath(byte portNumber) => $"/iolinkmaster/port[{portNumber}]/iolinkdevice/status";

    public static string PortDeviceProductNamePath(byte portNumber) => $"/iolinkmaster/port[{portNumber}]/iolinkdevice/productname";

    public static string PortDeviceVendorIdPath(byte portNumber) => $"/iolinkmaster/port[{portNumber}]/iolinkdevice/vendorid";

    public static string PortDeviceIdPath(byte portNumber) => $"/iolinkmaster/port[{portNumber}]/iolinkdevice/deviceid";

    public static string PortMasterCycleTimeActualPath(byte portNumber) => $"/iolinkmaster/port[{portNumber}]/mastercycletime_actual";

    public static string PortModePath(byte portNumber) => $"/iolinkmaster/port[{portNumber}]/mode";

    public static string PortComSpeedPath(byte portNumber) => $"/iolinkmaster/port[{portNumber}]/comspeed";
}