namespace Vendors.Ifm;

using IOLinkNET.Vendors.Ifm;
using FluentAssertions;
using IOLinkNET.Vendors.Ifm.Data;

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
        var result = await client.GetDeviceAcyclicData(req, default);

        result.Should().NotBeNull();
        result.Data.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task CanGetDevicePdinDataAsync()
    {
        var client = IfmIoTCoreClientFactory.Create(_baseUrl);
        var req = new IfmIoTReadPdInRequest(3);
        var result = await client.GetDevicePdinData(req, default);

        result.Should().NotBeNull();
        result.Data.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task CanGetDevicePdoutDataAsync()
    {
        var client = IfmIoTCoreClientFactory.Create(_baseUrl);
        var req = new IfmIoTReadPdOutRequest(3);
        var result = await client.GetDevicePdoutData(req, default);

        result.Should().NotBeNull();
        result.Data.Value.Should().NotBeNull();
    }
}