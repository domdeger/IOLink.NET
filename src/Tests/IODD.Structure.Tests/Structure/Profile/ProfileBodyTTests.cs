namespace IODD.Structure.Tests.Profile
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Interfaces.Profile;
    using IOLinkNET.IODD.Structure.Profile;

    using NSubstitute;

    using Xunit;

    public class ProfileBodyTTests
    {
        private readonly ProfileBodyT _testClass;
        private readonly IDeviceIdentityT _deviceIdentity;
        private readonly IDeviceFunctionT _deviceFunction;

        public ProfileBodyTTests()
        {
            _deviceIdentity = Substitute.For<IDeviceIdentityT>();
            _deviceFunction = Substitute.For<IDeviceFunctionT>();
            _testClass = new ProfileBodyT(_deviceIdentity, _deviceFunction);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ProfileBodyT(_deviceIdentity, _deviceFunction);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_ProfileBodyT()
        {
            // Arrange
            var same = new ProfileBodyT(_deviceIdentity, _deviceFunction);
            var different = new ProfileBodyT(Substitute.For<IDeviceIdentityT>(), Substitute.For<IDeviceFunctionT>());

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
        public void DeviceIdentityIsInitializedCorrectly()
        {
            _testClass.DeviceIdentity.Should().BeSameAs(_deviceIdentity);
        }

        [Fact]
        public void DeviceFunctionIsInitializedCorrectly()
        {
            _testClass.DeviceFunction.Should().BeSameAs(_deviceFunction);
        }
    }
}