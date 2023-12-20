using IOLinkNET.Integration;
using IOLinkNET.Vendors.Ifm;


// First initialize a master connection with a master device that supports the IoT Core api e.g. IFM AL1350 using the provided ConnectionFactory.
var masterConnection = IfmIoTCoreMasterConnectionFactory.Create("http://192.168.0.113");

// Create a port reader with the master connection and configure IOLinkNET to use the public IODD finder api and its default conversion stack.
var portReader = PortReaderBuilder.NewPortReader()
    .WithMasterConnection(masterConnection)
    .WithConverterDefaults()
    .WithPublicIODDFinderApi()
    .Build();


// Initialize the port reader for port 1.
await portReader.InitializeForPortAsync(1);


Console.WriteLine("Reading Process & Acyclic data from configured master.");
while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
{
    // Read the process data from the device.
    var processData = await portReader.ReadConvertedProcessDataInAsync();

    // Read the parameter 19, subindex 0 from the device. It is device specific what this parameter represents or if it is even available.
    var parameter = await portReader.ReadConvertedParameterAsync(19, 0);
    // Print the process data to the console.
    Console.WriteLine(processData);

    // Wait 100ms before reading the process data again.
    await Task.Delay(100);
}

