using System.Text.Json.Nodes;

namespace IOLink.NET.Vendors.Ifm.Tests.Data;

public class IfmIoTCoreResponsesTests
{
    [Fact]
    public void IfmIoTCoreResponseBase_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var data = "test-data";
        var cid = 123;
        var code = 456;

        // Act
        var response = new IfmIoTCoreResponseBase<string>(data, cid, code);

        // Assert
        response.Data.ShouldBe(data);
        response.Cid.ShouldBe(cid);
        response.Code.ShouldBe(code);
    }

    [Fact]
    public void IfmIoTCoreValueWrapper_Constructor_SetsValueCorrectly()
    {
        // Arrange
        var value = "wrapped-value";

        // Act
        var wrapper = new IfmIoTCoreValueWrapper<string>(value);

        // Assert
        wrapper.Value.ShouldBe(value);
    }

    [Fact]
    public void IfmIoTCoreScalarResponse_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var wrapper = new IfmIoTCoreValueWrapper<string>("test-value");
        var cid = 789;
        var code = 200;

        // Act
        var response = new IfmIoTCoreScalarResponse<string>(wrapper, cid, code);

        // Assert
        response.Data.ShouldBe(wrapper);
        response.Cid.ShouldBe(cid);
        response.Code.ShouldBe(code);
    }

    [Fact]
    public void IfmIoTCoreComplexResponse_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var data = new Dictionary<string, int> { { "key1", 1 }, { "key2", 2 } };
        var cid = 999;
        var code = 201;

        // Act
        var response = new IfmIoTCoreComplexResponse<Dictionary<string, int>>(data, cid, code);

        // Assert
        response.Data.ShouldBe(data);
        response.Cid.ShouldBe(cid);
        response.Code.ShouldBe(code);
    }

    [Fact]
    public void IfmIoTCoreGetDataMultiEntry_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var code = 100;
        var jsonValue = JsonValue.Create("test-json-value");

        // Act
        var entry = new IfmIoTCoreGetDataMultiEntry(code, jsonValue!);

        // Assert
        entry.Code.ShouldBe(code);
        entry.Data.ShouldBe(jsonValue);
    }

    [Fact]
    public void IfmIoTCorePortTreeResponse_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var treeStructure = new IfmIoTCoreTreeStructure(null, "root");
        var cid = 555;
        var code = 202;

        // Act
        var response = new IfmIoTCorePortTreeResponse(treeStructure, cid, code);

        // Assert
        response.Data.ShouldBe(treeStructure);
        response.Cid.ShouldBe(cid);
        response.Code.ShouldBe(code);
    }

    [Fact]
    public void IfmIoTCoreTreeStructure_Constructor_WithoutSubs_SetsPropertiesCorrectly()
    {
        // Arrange
        var identifier = "leaf-node";

        // Act
        var structure = new IfmIoTCoreTreeStructure(null, identifier);

        // Assert
        structure.Subs.ShouldBeNull();
        structure.Identifier.ShouldBe(identifier);
    }

    [Fact]
    public void IfmIoTCoreTreeStructure_Constructor_WithSubs_SetsPropertiesCorrectly()
    {
        // Arrange
        var child1 = new IfmIoTCoreTreeStructure(null, "child1");
        var child2 = new IfmIoTCoreTreeStructure(null, "child2");
        var subs = new[] { child1, child2 };
        var identifier = "parent-node";

        // Act
        var structure = new IfmIoTCoreTreeStructure(subs, identifier);

        // Assert
        structure.Subs.ShouldBe(subs);
        structure.Subs!.Count().ShouldBe(2);
        structure.Identifier.ShouldBe(identifier);
    }

    [Fact]
    public void IfmIoTCoreTreeStructure_NestedStructure_WorksCorrectly()
    {
        // Arrange
        var grandChild = new IfmIoTCoreTreeStructure(null, "grandchild");
        var child = new IfmIoTCoreTreeStructure(new[] { grandChild }, "child");
        var root = new IfmIoTCoreTreeStructure(new[] { child }, "root");

        // Act & Assert
        root.Identifier.ShouldBe("root");
        root.Subs.ShouldNotBeNull();
        root.Subs!.First().Identifier.ShouldBe("child");
        root.Subs.First().Subs.ShouldNotBeNull();
        root.Subs.First().Subs!.First().Identifier.ShouldBe("grandchild");
        root.Subs.First().Subs!.First().Subs.ShouldBeNull();
    }

    [Theory]
    [InlineData(42)]
    [InlineData("string-value")]
    [InlineData(true)]
    public void IfmIoTCoreValueWrapper_GenericTypes_WorkCorrectly<T>(T value)
    {
        // Act
        var wrapper = new IfmIoTCoreValueWrapper<T>(value);

        // Assert
        wrapper.Value.ShouldBe(value);
    }

    [Fact]
    public void IfmIoTCoreScalarResponse_InheritsFromResponseBase()
    {
        // Arrange
        var wrapper = new IfmIoTCoreValueWrapper<int>(42);
        var response = new IfmIoTCoreScalarResponse<int>(wrapper, 1, 2);

        // Act & Assert
        response.ShouldBeAssignableTo<IfmIoTCoreResponseBase<IfmIoTCoreValueWrapper<int>>>();
    }

    [Fact]
    public void IfmIoTCoreComplexResponse_InheritsFromResponseBase()
    {
        // Arrange
        var data = new List<string> { "item1", "item2" };
        var response = new IfmIoTCoreComplexResponse<List<string>>(data, 1, 2);

        // Act & Assert
        response.ShouldBeAssignableTo<IfmIoTCoreResponseBase<List<string>>>();
    }

    [Fact]
    public void IfmIoTCorePortTreeResponse_InheritsFromComplexResponse()
    {
        // Arrange
        var treeStructure = new IfmIoTCoreTreeStructure(null, "test");
        var response = new IfmIoTCorePortTreeResponse(treeStructure, 1, 2);

        // Act & Assert
        response.ShouldBeAssignableTo<IfmIoTCoreComplexResponse<IfmIoTCoreTreeStructure>>();
    }
}
