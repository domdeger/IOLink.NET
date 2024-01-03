namespace IODD.Structure.Tests.Structure.Datatypes
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Structure.Datatypes;

    using Xunit;

    public class TextDefinitionTTests
    {
        private readonly TextDefinitionT _testClass;
        private readonly string _id;
        private readonly string _value;

        public TextDefinitionTTests()
        {
            _id = "TestValue368833599";
            _value = "TestValue1371914282";
            _testClass = new TextDefinitionT(_id, _value);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new TextDefinitionT(_id, _value);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_TextDefinitionT()
        {
            // Arrange
            var same = new TextDefinitionT(_id, _value);
            var different = new TextDefinitionT("TestValue918454326", "TestValue551469764");

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
        public void ValueIsInitializedCorrectly()
        {
            _testClass.Value.Should().Be(_value);
        }
    }
}