namespace IOLink.NET.Core.Tests.Contracts;

public class InterfaceContractTests
{
    [Fact]
    public void IDeviceInformation_HasCorrectProperties()
    {
        // Arrange
        var interfaceType = typeof(IDeviceInformation);

        // Act & Assert
        interfaceType.GetProperty(nameof(IDeviceInformation.VendorId)).ShouldNotBeNull();
        interfaceType.GetProperty(nameof(IDeviceInformation.DeviceId)).ShouldNotBeNull();
        interfaceType.GetProperty(nameof(IDeviceInformation.ProductId)).ShouldNotBeNull();

        // Verify property types
        interfaceType
            .GetProperty(nameof(IDeviceInformation.VendorId))!
            .PropertyType.ShouldBe(typeof(ushort));
        interfaceType
            .GetProperty(nameof(IDeviceInformation.DeviceId))!
            .PropertyType.ShouldBe(typeof(uint));
        interfaceType
            .GetProperty(nameof(IDeviceInformation.ProductId))!
            .PropertyType.ShouldBe(typeof(string));
    }

    [Fact]
    public void IPortInformation_HasCorrectProperties()
    {
        // Arrange
        var interfaceType = typeof(IPortInformation);

        // Act & Assert
        interfaceType.GetProperty(nameof(IPortInformation.Status)).ShouldNotBeNull();
        interfaceType.GetProperty(nameof(IPortInformation.PortNumber)).ShouldNotBeNull();
        interfaceType.GetProperty(nameof(IPortInformation.DeviceInformation)).ShouldNotBeNull();

        // Verify property types
        interfaceType
            .GetProperty(nameof(IPortInformation.Status))!
            .PropertyType.ShouldBe(typeof(PortStatus));
        interfaceType
            .GetProperty(nameof(IPortInformation.PortNumber))!
            .PropertyType.ShouldBe(typeof(byte));
        interfaceType
            .GetProperty(nameof(IPortInformation.DeviceInformation))!
            .PropertyType.ShouldBe(typeof(IDeviceInformation));
    }

    [Fact]
    public void IMasterConnection_HasCorrectMethods()
    {
        // Arrange
        var interfaceType = typeof(IMasterConnection);

        // Act & Assert
        var methods = interfaceType.GetMethods();

        // Verify all expected methods exist
        methods.Any(m => m.Name == nameof(IMasterConnection.GetPortCountAsync)).ShouldBeTrue();
        methods
            .Any(m => m.Name == nameof(IMasterConnection.GetPortInformationAsync))
            .ShouldBeTrue();
        methods
            .Any(m => m.Name == nameof(IMasterConnection.GetPortInformationsAsync))
            .ShouldBeTrue();
        methods.Any(m => m.Name == nameof(IMasterConnection.ReadIndexAsync)).ShouldBeTrue();
        methods.Any(m => m.Name == nameof(IMasterConnection.ReadProcessDataInAsync)).ShouldBeTrue();
        methods
            .Any(m => m.Name == nameof(IMasterConnection.ReadProcessDataOutAsync))
            .ShouldBeTrue();
    }

    [Fact]
    public void IMasterConnection_GetPortCountAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IMasterConnection);
        var method = interfaceType.GetMethod(nameof(IMasterConnection.GetPortCountAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<byte>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(1);
        parameters[0].ParameterType.ShouldBe(typeof(CancellationToken));
        parameters[0].HasDefaultValue.ShouldBeTrue();
    }

    [Fact]
    public void IMasterConnection_GetPortInformationAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IMasterConnection);
        var method = interfaceType.GetMethod(nameof(IMasterConnection.GetPortInformationAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<IPortInformation>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(2);
        parameters[0].ParameterType.ShouldBe(typeof(byte));
        parameters[0].Name.ShouldBe("portNumber");
        parameters[1].ParameterType.ShouldBe(typeof(CancellationToken));
        parameters[1].HasDefaultValue.ShouldBeTrue();
    }

    [Fact]
    public void IMasterConnection_ReadIndexAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IMasterConnection);
        var method = interfaceType.GetMethod(nameof(IMasterConnection.ReadIndexAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<ReadOnlyMemory<byte>>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(4);
        parameters[0].ParameterType.ShouldBe(typeof(byte));
        parameters[0].Name.ShouldBe("portNumber");
        parameters[1].ParameterType.ShouldBe(typeof(ushort));
        parameters[1].Name.ShouldBe("index");
        parameters[2].ParameterType.ShouldBe(typeof(byte));
        parameters[2].Name.ShouldBe("subIindex");
        parameters[2].HasDefaultValue.ShouldBeTrue();
        parameters[2].DefaultValue.ShouldBe((byte)0);
        parameters[3].ParameterType.ShouldBe(typeof(CancellationToken));
        parameters[3].HasDefaultValue.ShouldBeTrue();
    }

    [Fact]
    public void IIODDProvider_HasCorrectMethods()
    {
        // Arrange
        var interfaceType = typeof(IIODDProvider);

        // Act & Assert
        var methods = interfaceType.GetMethods();
        methods.Any(m => m.Name == nameof(IIODDProvider.GetIODDPackageAsync)).ShouldBeTrue();
    }

    [Fact]
    public void IIODDProvider_GetIODDPackageAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IIODDProvider);
        var method = interfaceType.GetMethod(nameof(IIODDProvider.GetIODDPackageAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<Stream>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(4);
        parameters[0].ParameterType.ShouldBe(typeof(ushort));
        parameters[0].Name.ShouldBe("vendorId");
        parameters[1].ParameterType.ShouldBe(typeof(uint));
        parameters[1].Name.ShouldBe("deviceId");
        parameters[2].ParameterType.ShouldBe(typeof(string));
        parameters[2].Name.ShouldBe("productId");
        parameters[3].ParameterType.ShouldBe(typeof(CancellationToken));
        parameters[3].HasDefaultValue.ShouldBeTrue();
    }

    [Fact]
    public void IDeviceDefinitionProvider_HasCorrectMethods()
    {
        // Arrange
        var interfaceType = typeof(IDeviceDefinitionProvider<>);

        // Act & Assert
        var methods = interfaceType.GetMethods();
        methods
            .Any(m => m.Name == nameof(IDeviceDefinitionProvider<object>.GetDeviceDefinitionAsync))
            .ShouldBeTrue();
    }

    [Fact]
    public void IDeviceDefinitionProvider_GetDeviceDefinitionAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IDeviceDefinitionProvider<string>); // Using string as generic type parameter
        var method = interfaceType.GetMethod(
            nameof(IDeviceDefinitionProvider<string>.GetDeviceDefinitionAsync)
        );

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<string>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(4);
        parameters[0].ParameterType.ShouldBe(typeof(ushort));
        parameters[0].Name.ShouldBe("vendorId");
        parameters[1].ParameterType.ShouldBe(typeof(uint));
        parameters[1].Name.ShouldBe("deviceId");
        parameters[2].ParameterType.ShouldBe(typeof(string));
        parameters[2].Name.ShouldBe("productId");
        parameters[3].ParameterType.ShouldBe(typeof(CancellationToken));
        parameters[3].HasDefaultValue.ShouldBeTrue();
    }
}
