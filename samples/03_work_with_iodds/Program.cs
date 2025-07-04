using System.Xml.Linq;
using IOLinkNET.IODD;
using IOLinkNET.IODD.Structure.Datatypes;
using IOLinkNET.IODD.Structure.Structure.Menu;

// Load the IODD file and parse it.
IODDParser parser = new();
var iodd = parser.Parse(
    XElement.Load("./iodds/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml")
);

var parameters = iodd.ProfileBody.DeviceFunction.VariableCollection;

PrintDeviceInformation();
AnalyzeDeviceVariables();
AnalyzeProcessData();
DisplayMenuStructure();

void PrintDeviceInformation()
{
    Console.WriteLine(
        $"Vendor: {iodd.ProfileBody.DeviceIdentity.VendorName} ({iodd.ProfileBody.DeviceIdentity.VendorId}), Device: {iodd.ProfileBody.DeviceIdentity.DeviceId}"
    );
}

void AnalyzeDeviceVariables()
{
    Console.WriteLine($"Device has {parameters.Count()} parameters:");
    foreach (var parameter in parameters)
    {
        Console.WriteLine($"\t{parameter.Name.TextId} ({parameter.Index})");
    }

    var parameterWithUnresolvedData = parameters.First(p => p.Index == 147);

    Console.WriteLine(
        $"\nParameter with unresolved data type: {parameterWithUnresolvedData.Name.TextId}:"
    );

    var parameterDataType = parameterWithUnresolvedData.Datatype as RecordT;
    foreach (var field in parameterDataType!.Items)
    {
        Console.WriteLine(
            $"\t{field.Name.TextId} DataType: ({field.Type}), DataTypeRef: {field.Ref}"
        );
    }
}

void AnalyzeProcessData()
{
    var processData = iodd.ProfileBody.DeviceFunction.ProcessDataCollection;
    if (processData.Count() == 1)
    {
        Console.WriteLine("Device has no process data condition.");

        var processDataType = processData.First().ProcessDataIn!.Datatype as RecordT;
        foreach (var field in processDataType!.Items)
        {
            Console.WriteLine(
                $"\t{field.Name.TextId} DataType: ({field.Type}), DataTypeRef: {field.Ref}"
            );
        }
    }
    else
    {
        var condition = processData.First(pd => pd.Condition is not null).Condition;
        Console.WriteLine($"Device has process data condition: {condition!.VariableId}");
    }
}

void DisplayMenuStructure()
{
    var menuStructure = iodd.ProfileBody.DeviceFunction.UserInterface;
    Console.WriteLine($"Device has {menuStructure.MenuCollection.Count()} menus:");

    foreach (var menu in menuStructure.MenuCollection)
    {
        Console.WriteLine($"\t{menu.Menu.Name}");
        foreach (var subMenu in menu.Menu.MenuRefs ?? Enumerable.Empty<UIMenuRefT>())
        {
            Console.WriteLine($"\t\t{subMenu.MenuId}");
        }
    }
}
