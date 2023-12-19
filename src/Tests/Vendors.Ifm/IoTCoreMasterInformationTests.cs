namespace Vendors.Ifm;

using IOLinkNET.Vendors.Ifm;
using FluentAssertions;
using IOLinkNET.Vendors.Ifm.Data;

[Trait("Category", "IntegrationTest")]
public class IoTCoreMasterInformationTests
{
    private readonly string _baseUrl = "http://192.168.2.227/";
    [Fact]
    public async Task CanGetMasterDeviceTagAsync()
    {
        var client = IfmIoTCoreClientFactory.Create(_baseUrl);
        var result = await client.GetMasterDeviceTagAsync(default);

        result.Should().NotBeNull();
        result.Data.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task CanGetDeviceAcyclicDataAsync()
    {
        var client = IfmIoTCoreClientFactory.Create(_baseUrl);
        var req = new IfmIoTReadAcyclicRequest(3, 18, 0);
        var result = await client.GetDeviceAcyclicDataAsync(req, default);

        result.Should().NotBeNull();
        result.Data.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task CanGetDevicePdinDataAsync()
    {
        var client = IfmIoTCoreClientFactory.Create(_baseUrl);
        var req = new IfmIoTReadPdInRequest(3);
        var result = await client.GetDevicePdinDataAsync(req, default);

        result.Should().NotBeNull();
        result.Data.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task CanGetDevicePdoutDataAsync()
    {
        var client = IfmIoTCoreClientFactory.Create(_baseUrl);
        var req = new IfmIoTReadPdOutRequest(3);
        var result = await client.GetDevicePdoutDataAsync(req, default);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task CanGetDataMultiAsync()
    {
        var client = IfmIoTCoreClientFactory.Create(_baseUrl);
        var req = new IfmIoTGetDataMultiRequest(new[] { "/processdatamaster/temperature", "/deviceinfo/serialnumber" });
        var result = await client.GetDataMultiAsync(req, default);

        result.Should().NotBeNull();

    }
}