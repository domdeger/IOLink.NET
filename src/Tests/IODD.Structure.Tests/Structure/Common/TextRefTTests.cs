namespace IODD.Structure.Tests.Common
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Common;

    using Xunit;

    public class TextRefTTests
    {
        private readonly TextRefT _testClass;
        private readonly string _textId;

        public TextRefTTests()
        {
            _textId = "TestValue1435933271";
            _testClass = new TextRefT(_textId);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new TextRefT(_textId);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_TextRefT()
        {
            // Arrange
            var same = new TextRefT(_textId);
            var different = new TextRefT("TestValue374191719");

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
        public void TextIdIsInitializedCorrectly()
        {
            _testClass.TextId.Should().Be(_textId);
        }
    }
}