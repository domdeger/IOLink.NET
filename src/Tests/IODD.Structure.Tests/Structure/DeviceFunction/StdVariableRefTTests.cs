namespace IODD.Structure.Tests.DeviceFunction
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.DeviceFunction;

    using Xunit;

    public class StdVariableRefTTests
    {
        private readonly StdVariableRefT _testClass;
        private readonly string _id;
        private readonly uint _fixedLengthRestriction;
        private readonly string _defaultValue;

        public StdVariableRefTTests()
        {
            _id = "TestValue1875853135";
            _fixedLengthRestriction = (uint)788213752;
            _defaultValue = "TestValue1741289056";
            _testClass = new StdVariableRefT(_id, _fixedLengthRestriction, _defaultValue);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new StdVariableRefT(_id, _fixedLengthRestriction, _defaultValue);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_StdVariableRefT()
        {
            // Arrange
            var same = new StdVariableRefT(_id, _fixedLengthRestriction, _defaultValue);
            var different = new StdVariableRefT("TestValue1462138130", (uint)1858438991, "TestValue648640188");

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
        public void IdIsInitializedCorrectly()
        {
            _testClass.Id.Should().Be(_id);
        }

        [Fact]
        public void FixedLengthRestrictionIsInitializedCorrectly()
        {
            _testClass.FixedLengthRestriction.Should().Be(_fixedLengthRestriction);
        }

        [Fact]
        public void defaultValueIsInitializedCorrectly()
        {
            _testClass.defaultValue.Should().Be(_defaultValue);
        }
    }
}