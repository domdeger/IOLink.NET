using IOLinkNET.IODD.Structure;

namespace IOLinkNET.IODD.Resolution.Contracts;

public interface ITypeResolverFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="IProcessDataTypeResolver"/> for the given <see cref="IODevice"/>.
    /// </summary>
    /// <param name="deviceDefinition">The device definition.</param>
    /// <returns>The process data type resolver.</returns>
    IProcessDataTypeResolver CreateProcessDataTypeResolver(IODevice deviceDefinition);
    /// <summary>
    /// Creates a new instance of <see cref="IParameterTypeResolver"/> for the given <see cref="IODevice"/>.
    /// </summary>
    /// <param name="deviceDefinition">The device definition.</param>
    /// <returns></returns>
    IParameterTypeResolver CreateParameterTypeResolver(IODevice deviceDefinition);
}