using IOLinkNET.IODD.Resolution.Contracts;
using IOLinkNET.IODD.Structure;

namespace IOLinkNET.IODD.Resolution.Common;

public class DefaultTypeResolverFactory : ITypeResolverFactory
{
    public IProcessDataTypeResolver CreateProcessDataTypeResolver(IODevice deviceDefinition)
        => new ProcessDataTypeResolver(deviceDefinition);

    public IParameterTypeResolver CreateParameterTypeResolver(IODevice deviceDefinition)
        => new ParameterTypeResolver(deviceDefinition);
}