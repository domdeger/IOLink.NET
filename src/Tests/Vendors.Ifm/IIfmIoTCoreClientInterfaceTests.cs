using Refit;

namespace IOLink.NET.Vendors.Ifm.Tests;

public class IIfmIoTCoreClientInterfaceTests
{
    [Fact]
    public void IIfmIoTCoreClient_IsPublicInterface()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);

        // Act & Assert
        interfaceType.IsInterface.ShouldBeTrue();
        interfaceType.IsPublic.ShouldBeTrue();
    }

    [Fact]
    public void GetMasterDeviceTagAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var method = interfaceType.GetMethod(nameof(IIfmIoTCoreClient.GetMasterDeviceTagAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<IfmIoTCoreScalarResponse<string>>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(1);
        parameters[0].ParameterType.ShouldBe(typeof(CancellationToken));
    }

    [Fact]
    public void GetMasterDeviceTagAsync_HasGetAttribute()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var method = interfaceType.GetMethod(nameof(IIfmIoTCoreClient.GetMasterDeviceTagAsync));

        // Act
        var getAttribute = method!.GetCustomAttribute<GetAttribute>();

        // Assert
        getAttribute.ShouldNotBeNull();
        getAttribute!.Path.ShouldBe("/devicetag/applicationtag/getdata");
    }

    [Fact]
    public void GetDeviceAcyclicDataAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var method = interfaceType.GetMethod(nameof(IIfmIoTCoreClient.GetDeviceAcyclicDataAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<IfmIoTCoreScalarResponse<string>>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(2);
        parameters[0].ParameterType.ShouldBe(typeof(IfmIoTReadAcyclicRequest));
        parameters[0].Name.ShouldBe("request");
        parameters[1].ParameterType.ShouldBe(typeof(CancellationToken));
        parameters[1].Name.ShouldBe("cancellationToken");
    }

    [Fact]
    public void GetDeviceAcyclicDataAsync_HasPostAttribute()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var method = interfaceType.GetMethod(nameof(IIfmIoTCoreClient.GetDeviceAcyclicDataAsync));

        // Act
        var postAttribute = method!.GetCustomAttribute<PostAttribute>();

        // Assert
        postAttribute.ShouldNotBeNull();
        postAttribute!.Path.ShouldBe("");
    }

    [Fact]
    public void GetDevicePdinDataAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var method = interfaceType.GetMethod(nameof(IIfmIoTCoreClient.GetDevicePdinDataAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<IfmIoTCoreScalarResponse<string>>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(2);
        parameters[0].ParameterType.ShouldBe(typeof(IfmIoTReadPdInRequest));
        parameters[1].ParameterType.ShouldBe(typeof(CancellationToken));
    }

    [Fact]
    public void GetDevicePdoutDataAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var method = interfaceType.GetMethod(nameof(IIfmIoTCoreClient.GetDevicePdoutDataAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<IfmIoTCoreScalarResponse<string>>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(2);
        parameters[0].ParameterType.ShouldBe(typeof(IfmIoTReadPdOutRequest));
        parameters[1].ParameterType.ShouldBe(typeof(CancellationToken));
    }

    [Fact]
    public void GetDataMultiAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var method = interfaceType.GetMethod(nameof(IIfmIoTCoreClient.GetDataMultiAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(
            typeof(Task<IfmIoTCoreComplexResponse<Dictionary<string, IfmIoTCoreGetDataMultiEntry>>>)
        );

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(2);
        parameters[0].ParameterType.ShouldBe(typeof(IfmIoTGetDataMultiRequest));
        parameters[1].ParameterType.ShouldBe(typeof(CancellationToken));
    }

    [Fact]
    public void GetPortTreeAsync_HasCorrectSignature()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var method = interfaceType.GetMethod(nameof(IIfmIoTCoreClient.GetPortTreeAsync));

        // Act & Assert
        method.ShouldNotBeNull();
        method!.ReturnType.ShouldBe(typeof(Task<IfmIoTCorePortTreeResponse>));

        var parameters = method.GetParameters();
        parameters.Length.ShouldBe(2);
        parameters[0].ParameterType.ShouldBe(typeof(IfmIoTGetPortTreeRequest));
        parameters[1].ParameterType.ShouldBe(typeof(CancellationToken));
    }

    [Fact]
    public void AllAsyncMethods_HavePostAttribute()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var postMethods = new[]
        {
            nameof(IIfmIoTCoreClient.GetDeviceAcyclicDataAsync),
            nameof(IIfmIoTCoreClient.GetDevicePdinDataAsync),
            nameof(IIfmIoTCoreClient.GetDevicePdoutDataAsync),
            nameof(IIfmIoTCoreClient.GetDataMultiAsync),
            nameof(IIfmIoTCoreClient.GetPortTreeAsync),
        };

        // Act & Assert
        foreach (var methodName in postMethods)
        {
            var method = interfaceType.GetMethod(methodName);
            method.ShouldNotBeNull();

            var postAttribute = method!.GetCustomAttribute<PostAttribute>();
            postAttribute.ShouldNotBeNull($"Method {methodName} should have PostAttribute");
        }
    }

    [Fact]
    public void IIfmIoTCoreClient_AllMethods_ReturnTask()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var methods = interfaceType.GetMethods();

        // Act & Assert
        foreach (var method in methods)
        {
            method.ReturnType.IsGenericType.ShouldBeTrue();
            method.ReturnType.GetGenericTypeDefinition().ShouldBe(typeof(Task<>));
        }
    }

    [Fact]
    public void IIfmIoTCoreClient_AllMethods_TakeCancellationToken()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var methods = interfaceType.GetMethods();

        // Act & Assert
        foreach (var method in methods)
        {
            var parameters = method.GetParameters();
            parameters
                .Last()
                .ParameterType.ShouldBe(
                    typeof(CancellationToken),
                    $"Method {method.Name} should have CancellationToken as last parameter"
                );
        }
    }

    [Fact]
    public void IIfmIoTCoreClient_HasExpectedMethodCount()
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);
        var methods = interfaceType.GetMethods();

        // Act & Assert
        methods.Length.ShouldBe(6); // All the async methods we expect
    }

    [Theory]
    [InlineData("GetMasterDeviceTagAsync")]
    [InlineData("GetDeviceAcyclicDataAsync")]
    [InlineData("GetDevicePdinDataAsync")]
    [InlineData("GetDevicePdoutDataAsync")]
    [InlineData("GetDataMultiAsync")]
    [InlineData("GetPortTreeAsync")]
    public void IIfmIoTCoreClient_HasExpectedMethod(string methodName)
    {
        // Arrange
        var interfaceType = typeof(IIfmIoTCoreClient);

        // Act
        var method = interfaceType.GetMethod(methodName);

        // Assert
        method.ShouldNotBeNull();
    }
}
