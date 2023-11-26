// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using IOLinkNET.Conversion;
using IOLinkNET.IODD;
using IOLinkNET.IODD.Resolution;


// First we need to parse an IODD definition. This can be done by using the IODDParser class. Or by directly retrieving the IODD from the IODD finder with the IODDFinderPublicClient class.
IODDParser parser = new();
var device = parser.Parse(XElement.Load("../iodds/Balluff-BISM4A308240107S4-CCM-20210928-IODD1.1.xml"));

// Now we can use the IoddConverter class to convert the data.
var converter = new IoddConverter();

DecodeProcessData();
DecodeParameterData();

void DecodeParameterData()
{

    // Retrieve the parameter data from the device
    var data = Convert.FromBase64String("SGVsbG9IZWxsb0hlbGxvSGVsbG9IZWxsb0hlbGxvSGVsbG9IZWxsbw==");

    // Get convertible data type definition for requested parameter with index 25 from parameter type resolver
    var parameterTypeResolver = new ParameterTypeResolver(device);
    var convertibleType = parameterTypeResolver.GetParameter(25);

    // Convert the data
    var convertedParameterData = converter.Convert(convertibleType, data);

    // if we do not know whether we retrieve a scalar or a list, we can check the type in this case we know it is a scalar
    Console.WriteLine($"Decoded parameter data {convertedParameterData}");
}

void DecodeProcessData()
{
    // retrieve the process data from the device
    var data = Convert.FromBase64String("gAEDAAAAAAAAgAA=");

    // Get convertible data type definition for process data from process data type resolver
    var pdResolver = new ProcessDataTypeResolver(device);
    var convertibleType = pdResolver.ResolveProcessDataIn();

    // Convert the data
    var pd = converter.Convert(convertibleType, data) as List<(string, object)>;

    // Print the result, we know that the result is a list of tuples with string and object because it is a RecordT from the IODD definition
    Console.WriteLine("Decoded parameter data:");
    foreach (var item in pd)
    {
        Console.WriteLine($"{item.Item1}: {item.Item2}");
    }


}
