using IOLink.NET.IODD.Resolution;
using IOLink.NET.IODD.Resolution.Contracts;
using IOLink.NET.IODD.Structure;

namespace IOLink.NET.Integration;

/// <summary>
/// Contains all the context information for an initialized port.
/// </summary>
/// <param name="Port">The port number.</param>
/// <param name="DeviceDefinition">The resolved device definition.</param>
/// <param name="ProcessDataTypeResolver">The process data type resolver.</param>
/// <param name="ParameterTypeResolver">The parameter type resolver.</param>
/// <param name="PdIn">The resolved process data input type, if available.</param>
/// <param name="PdOut">The resolved process data output type, if available.</param>
public record PortContext(
    byte Port,
    IODevice DeviceDefinition,
    IProcessDataTypeResolver ProcessDataTypeResolver,
    IParameterTypeResolver ParameterTypeResolver,
    ParsableDatatype? PdIn,
    ParsableDatatype? PdOut
);
