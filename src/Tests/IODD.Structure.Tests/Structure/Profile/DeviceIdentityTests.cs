namespace IODD.Structure.Tests.Profile
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Common;
    using IOLinkNET.IODD.Structure.Profile;

    using Xunit;

    public class DeviceIdentityTTests
    {
        private readonly DeviceIdentityT _testClass;
        private readonly ushort _vendorId;
        private readonly uint _deviceId;
        private readonly string _vendorName;
        private readonly TextRefT _vendorText;
        private readonly TextRefT _vendorUrl;
        private readonly TextRefT _deviceName;
        private readonly TextRefT _deviceFamily;

        public DeviceIdentityTTests()
        {
            _vendorId = (ushort)61673;
            _deviceId = (uint)2100737468;
            _vendorName = "TestValue1518116962";
            _vendorText = new TextRefT("TestValue1244198989");
            _vendorUrl = new TextRefT("TestValue1928583565");
            _deviceName = new TextRefT("TestValue126587864");
            _deviceFamily = new TextRefT("TestValue511745906");
            _testClass = new DeviceIdentityT(_vendorId, _deviceId, _vendorName, _vendorText, _vendorUrl, _deviceName, _deviceFamily);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new DeviceIdentityT(_vendorId, _deviceId, _vendorName, _vendorText, _vendorUrl, _deviceName, _deviceFamily);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_DeviceIdentityT()
        {
            // Arrange
            var same = new DeviceIdentityT(_vendorId, _deviceId, _vendorName, _vendorText, _vendorUrl, _deviceName, _deviceFamily);
            var different = new DeviceIdentityT((ushort)40545, (uint)284608158, "TestValue1686782123", new TextRefT("TestValue1634490598"), new TextRefT("TestValue1965129852"), new TextRefT("TestValue340540105"), new TextRefT("TestValue605090797"));

            // Assert
            _testClass?.Equals(default(object)).Should().BeFalse();
            _testClass?.Equals(new object()).Should().BeFalse();
            _testClass?.Equals((object)same).Should().BeTrue();
            _testClass?.Equals((object)different).Should().BeFalse();
            _testClass?.Equals(same).Should().BeTrue();
            _testClass?.Equals(different).Should().BeFalse();
            _testClass?.GetHashCode().Should().Be(same.GetHashCode());
            _testClass?.GetHashCode().Should().NotBe(different.GetHashCode());
            (_testClass == same).Should().BeTrue();
            (_testClass == different).Should().BeFalse();
            (_testClass != same).Should().BeFalse();
            (_testClass != different).Should().BeTrue();
        }

        [Fact]
        public void VendorIdIsInitializedCorrectly()
        {
            _testClass.VendorId.Should().Be(_vendorId);
        }

        [Fact]
        public void DeviceIdIsInitializedCorrectly()
        {
            _testClass.DeviceId.Should().Be(_deviceId);
        }

        [Fact]
        public void VendorNameIsInitializedCorrectly()
        {
            _testClass.VendorName.Should().Be(_vendorName);
        }

        [Fact]
        public void VendorTextIsInitializedCorrectly()
        {
            _testClass.VendorText.Should().BeSameAs(_vendorText);
        }

        [Fact]
        public void VendorUrlIsInitializedCorrectly()
        {
            _testClass.VendorUrl.Should().BeSameAs(_vendorUrl);
        }

        [Fact]
        public void DeviceNameIsInitializedCorrectly()
        {
            _testClass.DeviceName.Should().BeSameAs(_deviceName);
        }

        [Fact]
        public void DeviceFamilyIsInitializedCorrectly()
        {
            _testClass.DeviceFamily.Should().BeSameAs(_deviceFamily);
        }
    }
}