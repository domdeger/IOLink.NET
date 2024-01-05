using FluentAssertions;

using IOLinkNET.Vendors.Ifm;
using IOLinkNET.Vendors.Ifm.Data;

using Vendors.Ifm.Configuration;

namespace Vendors.Ifm;

[Trait("Category", "IntegrationTest")]
[Collection("IfmIoTCoreIntegrationTest")]
[CollectionDefinition("IfmIoTCoreIntegrationTest", DisableParallelization = true)]
public class IfmIoTCoreMasterInformationTests
{
    private readonly string _baseUrl = $"http://{MasterConfiguration.IP}/";
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

    [Fact(Skip = "Devices not always have PDOut data")]
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

    [Fact]
    public async Task CanGetPortTreeAsync()
    {
        var client = IfmIoTCoreClientFactory.Create(_baseUrl);
        var req = new IfmIoTGetPortTreeRequest();
        var result = await client.GetPortTreeAsync(req, default);

        result.Should().NotBeNull();
    }
}