// First initialize a master connection with a master device that supports the IoT Core api e.g. IFM AL1350 using the provided ConnectionFactory.
using IOLink.NET.Integration;
using IOLink.NET.Vendors.Ifm;

var masterConnection = IfmIoTCoreMasterConnectionFactory.Create("http://192.168.1.177");

// Create a port reader with the master connection and configure IOLinkNET to use the public IODD finder api and its default conversion stack.
var portReader = PortReaderBuilder
    .NewPortReader()
    .WithMasterConnection(masterConnection)
    .WithConverterDefaults()
    .WithPublicIODDFinderApi()
    .Build();

// Initialize the port reader for port 1.
await portReader.InitializeForPortAsync(4, CancellationToken.None);

Console.WriteLine("Reading Process & Acyclic data from configured master.");
while (true)
{
    // Read the process data from the device.
    var processData = await portReader.ReadConvertedProcessDataInAsync(CancellationToken.None);

    // Read the parameter 19, subindex 0 from the device. It is device specific what this parameter represents or if it is even available.
    var parameter = await portReader.ReadConvertedParameterAsync(19, 0, CancellationToken.None);
    if (parameter is List<ValueTuple<string, object>> parameterValues)
    {
        // Print the parameter values to the console.
        Console.WriteLine("Parameter 19, Subindex 0:");
        foreach (var value in parameterValues)
        {
            Console.WriteLine($"  {value.Item1}: {value.Item2}");
        }
    }
    else
    {
        Console.WriteLine("Parameter 19, Subindex 0 is not available or not convertible.");
    }
    // Print the process data to the console.
    Console.WriteLine(processData);

    // Try to detect an Escape key press, but avoid calling Console.KeyAvailable when there's no console
    // or when input is redirected. Use Console.IsInputRedirected as a guard and fall back to catching
    // InvalidOperationException just in case.
    try
    {
        if (!Console.IsInputRedirected && Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
                break;
        }
    }
    catch (InvalidOperationException)
    {
        // No console available or input redirected - continue without key checking.
    }

    // Wait 500ms before reading the process data again.
    await Task.Delay(500);
}
