namespace IOLink.NET.Vendors.Ifm.Tests.Data;

public class IfmIoTCoreRequestsTests
{
    [Fact]
    public void IfmIoTCoreServiceRequestBase_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var adr = "test-address";
        var code = "test-code";
        var cid = 42;

        // Act
        var request = new IfmIoTCoreServiceRequestBase(adr, code, cid);

        // Assert
        request.Adr.ShouldBe(adr);
        request.Code.ShouldBe(code);
        request.Cid.ShouldBe(cid);
    }

    [Fact]
    public void IfmIoTCoreServiceRequestBase_DefaultValues_AreCorrect()
    {
        // Arrange
        var adr = "test-address";

        // Act
        var request = new IfmIoTCoreServiceRequestBase(adr);

        // Assert
        request.Adr.ShouldBe(adr);
        request.Code.ShouldBe("request");
        request.Cid.ShouldBe(1337);
    }

    [Fact]
    public void IfmIoTCoreServiceParameterizedRequest_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var adr = "test-address";
        var data = "test-data";
        var code = "custom-code";
        var cid = 999;

        // Act
        var request = new IfmIoTCoreServiceParameterizedRequest<string>(adr, data, code, cid);

        // Assert
        request.Adr.ShouldBe(adr);
        request.Data.ShouldBe(data);
        request.Code.ShouldBe(code);
        request.Cid.ShouldBe(cid);
    }

    [Fact]
    public void IfmIoTReadAcyclicRequest_Constructor_SetsCorrectAddress()
    {
        // Arrange
        var port = 3;
        var index = 0x1234;
        var subindex = 0x56;

        // Act
        var request = new IfmIoTReadAcyclicRequest(port, index, subindex);

        // Assert
        request.Adr.ShouldBe("iolinkmaster/port[3]/iolinkdevice/iolreadacyclic");
        request.Data.ShouldNotBeNull();
        request.Data.index.ShouldBe(index);
        request.Data.subindex.ShouldBe(subindex);
    }

    [Fact]
    public void IfmIoTReadPdInRequest_Constructor_SetsCorrectAddress()
    {
        // Arrange
        var port = 5;

        // Act
        var request = new IfmIoTReadPdInRequest(port);

        // Assert
        request.Adr.ShouldBe("iolinkmaster/port[5]/iolinkdevice/pdin/getdata");
    }

    [Fact]
    public void IfmIoTReadPdOutRequest_Constructor_SetsCorrectAddress()
    {
        // Arrange
        var port = 7;

        // Act
        var request = new IfmIoTReadPdOutRequest(port);

        // Assert
        request.Adr.ShouldBe("iolinkmaster/port[7]/iolinkdevice/pdout/getdata");
    }

    [Fact]
    public void IfmIoTGetDataMultiRequest_Constructor_SetsCorrectProperties()
    {
        // Arrange
        var paths = new[] { "path1", "path2", "path3" };

        // Act
        var request = new IfmIoTGetDataMultiRequest(paths);

        // Assert
        request.Adr.ShouldBe("GetDataMulti");
        request.Data.ShouldNotBeNull();
        request.Data.Datatosend.ShouldBe(paths);
    }

    [Fact]
    public void IfmIoTGetPortTreeRequest_Constructor_SetsCorrectProperties()
    {
        // Act
        var request = new IfmIoTGetPortTreeRequest();

        // Assert
        request.Adr.ShouldBe("gettree");
        request.Data.ShouldNotBeNull();
        request.Data.Adr.ShouldBe("iolinkmaster/");
        request.Data.Level.ShouldBe(1);
    }

    [Theory]
    [InlineData(9, 0x01)] // Test with the expectedIndex parameter
    [InlineData(0xFFFF, 0xFF)]
    [InlineData(0, 0)]
    public void IfmIoTAcyclicParameters_Constructor_SetsPropertiesCorrectly(
        int expectedIndex,
        int? subindex
    )
    {
        // Act
        var parameters = new IfmIoTAcyclicParameters(expectedIndex, subindex);

        // Assert
        parameters.index.ShouldBe(expectedIndex);
        parameters.subindex.ShouldBe(subindex);
    }

    [Fact]
    public void IfmIoTGetDataMultiParameters_Constructor_SetsPropertiesCorrectly()
    {
        // Arrange
        var datatosend = new[] { "data1", "data2", "data3" };

        // Act
        var parameters = new IfmIoTGetDataMultiParameters(datatosend);

        // Assert
        parameters.Datatosend.ShouldBe(datatosend);
    }

    [Theory]
    [InlineData("test-address", 2)]
    [InlineData(null, null)]
    [InlineData("", 0)]
    public void IfmIoTGetTreeParameters_Constructor_SetsPropertiesCorrectly(string? adr, int? level)
    {
        // Act
        var parameters = new IfmIoTGetTreeParameters(adr, level);

        // Assert
        parameters.Adr.ShouldBe(adr);
        parameters.Level.ShouldBe(level);
    }
}
