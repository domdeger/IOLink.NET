namespace IODD.Structure.Tests.Common
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Common;

    using Xunit;

    public class DatatypeRefTTests
    {
        private readonly DatatypeRefT _testClass;
        private readonly string _datatypeId;

        public DatatypeRefTTests()
        {
            _datatypeId = "TestValue586053187";
            _testClass = new DatatypeRefT(_datatypeId);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new DatatypeRefT(_datatypeId);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_DatatypeRefT()
        {
            // Arrange
            var same = new DatatypeRefT(_datatypeId);
            var different = new DatatypeRefT("TestValue2043819047");

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
        public void DatatypeIdIsInitializedCorrectly()
        {
            _testClass.DatatypeId.Should().Be(_datatypeId);
        }
    }
}