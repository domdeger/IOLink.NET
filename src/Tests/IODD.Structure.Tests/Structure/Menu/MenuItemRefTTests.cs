namespace IODD.Structure.Tests.Structure.Menu
{
    using System;

    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Structure.Menu;

    using Xunit;

    public class MenuItemRefTTests
    {
        private readonly MenuItemRefT _testClass;
        private readonly string _id;
        private readonly string _name;

        public MenuItemRefTTests()
        {
            _id = "TestValue1269697157";
            _name = "TestValue473445743";
            _testClass = new MenuItemRefT(_id, _name);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new MenuItemRefT(_id, _name);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_MenuItemRefT()
        {
            // Arrange
            var same = new MenuItemRefT(_id, _name);
            var different = new MenuItemRefT("TestValue612465102", "TestValue2029103256");

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
        public void NameIsInitializedCorrectly()
        {
            _testClass.Name.Should().Be(_name);
        }
    }
}