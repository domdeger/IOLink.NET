using IOLink.NET.IODD.Resolution.Contracts;
using IOLink.NET.IODD.Structure;

namespace IOLink.NET.IODD.Resolution.Common;

public class DefaultTypeResolverFactory : ITypeResolverFactory
{
    public IProcessDataTypeResolver CreateProcessDataTypeResolver(IODevice deviceDefinition)
        => new ProcessDataTypeResolver(deviceDefinition);

    public IParameterTypeResolver CreateParameterTypeResolver(IODevice deviceDefinition)
        => new ParameterTypeResolver(deviceDefinition);
}
