namespace IODD.Structure.Tests.Structure.ExternalTextCollection
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;

    using Xunit;

    public class PrimaryLanguageTTests
    {
        private readonly PrimaryLanguageT _testClass;
        private readonly string _languageCode;

        public PrimaryLanguageTTests()
        {
            _languageCode = "TestValue711335815";
            _testClass = new PrimaryLanguageT(_languageCode);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new PrimaryLanguageT(_languageCode);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_PrimaryLanguageT()
        {
            // Arrange
            var same = new PrimaryLanguageT(_languageCode);
            var different = new PrimaryLanguageT("TestValue655708485");

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
        public void LanguageCodeIsInitializedCorrectly()
        {
            _testClass.LanguageCode.Should().Be(_languageCode);
        }
    }
}