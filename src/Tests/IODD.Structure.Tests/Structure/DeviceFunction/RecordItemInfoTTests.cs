namespace IODD.Structure.Tests.DeviceFunction
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.DeviceFunction;

    using Xunit;

    public class RecordItemInfoTTests
    {
        private readonly RecordItemInfoT _testClass;
        private readonly byte _subIndex;
        private readonly object _defaultValue;
        private readonly bool _modifiesOtherVariables;
        private readonly bool _excludedFromDataStorage;

        public RecordItemInfoTTests()
        {
            _subIndex = (byte)198;
            _defaultValue = new object();
            _modifiesOtherVariables = false;
            _excludedFromDataStorage = true;
            _testClass = new RecordItemInfoT(_subIndex, _defaultValue, _modifiesOtherVariables, _excludedFromDataStorage);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new RecordItemInfoT(_subIndex, _defaultValue, _modifiesOtherVariables, _excludedFromDataStorage);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_RecordItemInfoT()
        {
            // Arrange
            var same = new RecordItemInfoT(_subIndex, _defaultValue, _modifiesOtherVariables, _excludedFromDataStorage);
            var different = new RecordItemInfoT((byte)44, new object(), false, false);

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
        public void SubIndexIsInitializedCorrectly()
        {
            _testClass.SubIndex.Should().Be(_subIndex);
        }

        [Fact]
        public void DefaultValueIsInitializedCorrectly()
        {
            _testClass.DefaultValue.Should().BeSameAs(_defaultValue);
        }

        [Fact]
        public void ModifiesOtherVariablesIsInitializedCorrectly()
        {
            _testClass.ModifiesOtherVariables.Should().Be(_modifiesOtherVariables);
        }

        [Fact]
        public void ExcludedFromDataStorageIsInitializedCorrectly()
        {
            _testClass.ExcludedFromDataStorage.Should().Be(_excludedFromDataStorage);
        }
    }
}