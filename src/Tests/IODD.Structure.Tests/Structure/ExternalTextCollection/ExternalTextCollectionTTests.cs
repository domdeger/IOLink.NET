namespace IODD.Structure.Tests.Structure.ExternalTextCollection
{
    using System.Collections.Generic;

    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.Structure.ExternalTextCollection;

    using Xunit;

    public class ExternalTextCollectionTTests
    {
        private readonly ExternalTextCollectionT _testClass;
        private readonly PrimaryLanguageT _primaryLanguage;
        private readonly IEnumerable<TextDefinitionT> _textDefinitions;

        public ExternalTextCollectionTTests()
        {
            _primaryLanguage = new PrimaryLanguageT("TestValue1905909488");
            _textDefinitions = new[] { new TextDefinitionT("TestValue460042982", "TestValue485553547"), new TextDefinitionT("TestValue918849641", "TestValue276623235"), new TextDefinitionT("TestValue1673298970", "TestValue1137800201") };
            _testClass = new ExternalTextCollectionT(_primaryLanguage, _textDefinitions);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ExternalTextCollectionT(_primaryLanguage, _textDefinitions);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_ExternalTextCollectionT()
        {
            // Arrange
            var same = new ExternalTextCollectionT(_primaryLanguage, _textDefinitions);
            var different = new ExternalTextCollectionT(new PrimaryLanguageT("TestValue986210869"), new[] { new TextDefinitionT("TestValue1006471892", "TestValue114748907"), new TextDefinitionT("TestValue896792451", "TestValue534261184"), new TextDefinitionT("TestValue1876822631", "TestValue1302215722") });

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
        public void PrimaryLanguageIsInitializedCorrectly()
        {
            _testClass.PrimaryLanguage.Should().BeSameAs(_primaryLanguage);
        }

        [Fact]
        public void TextDefinitionsIsInitializedCorrectly()
        {
            _testClass.TextDefinitions.Should().BeSameAs(_textDefinitions);
        }
    }
}