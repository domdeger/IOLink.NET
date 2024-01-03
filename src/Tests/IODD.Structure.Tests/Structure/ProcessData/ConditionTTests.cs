namespace IODD.Structure.Tests.ProcessData
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.ProcessData;

    using Xunit;

    public class ConditionTTests
    {
        private readonly ConditionT _testClass;
        private readonly string _variableId;
        private readonly byte? _subindex;
        private readonly int _value;

        public ConditionTTests()
        {
            _variableId = "TestValue883403207";
            _subindex = (byte)173;
            _value = 1215636679;
            _testClass = new ConditionT(_variableId, _subindex, _value);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ConditionT(_variableId, _subindex, _value);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_ConditionT()
        {
            // Arrange
            var same = new ConditionT(_variableId, _subindex, _value);
            var different = new ConditionT("TestValue1577166359", (byte)106, 527257594);

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
        public void VariableIdIsInitializedCorrectly()
        {
            _testClass.VariableId.Should().Be(_variableId);
        }

        [Fact]
        public void SubindexIsInitializedCorrectly()
        {
            _testClass.Subindex.Should().Be(_subindex);
        }

        [Fact]
        public void ValueIsInitializedCorrectly()
        {
            _testClass.Value.Should().Be(_value);
        }
    }
}