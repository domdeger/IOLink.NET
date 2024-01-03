namespace IODD.Structure.Tests.Structure.Menu
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.Structure.Menu;

    using Xunit;

    public class UIDataItemRefTTests
    {
        private readonly UIDataItemRefT _testClass;
        private readonly string _variableId;
        private readonly decimal? _gradient;
        private readonly decimal? _offset;
        private readonly uint? _unitCode;
        private readonly AccessRightsT? _accessRights;
        private readonly string _buttonValue;
        private readonly DisplayFormat? _displayFormat;

        public UIDataItemRefTTests()
        {
            _variableId = "TestValue1802611606";
            _gradient = 720089547.21M;
            _offset = 168995190.87M;
            _unitCode = (uint)614295831;
            _accessRights = new AccessRightsT?();
            _buttonValue = "TestValue1216451171";
            _displayFormat = new DisplayFormat?();
            _testClass = new UIDataItemRefT(_variableId, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new UIDataItemRefT(_variableId, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_UIDataItemRefT()
        {
            // Arrange
            var same = new UIDataItemRefT(_variableId, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat);
            var different = new UIDataItemRefT("TestValue13314447", 517804737.12M, 1243818349.29M, (uint)493842155, new AccessRightsT?(), "TestValue1983885988", new DisplayFormat?());

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
        public void GradientIsInitializedCorrectly()
        {
            _testClass.Gradient.Should().Be(_gradient);
        }

        [Fact]
        public void OffsetIsInitializedCorrectly()
        {
            _testClass.Offset.Should().Be(_offset);
        }

        [Fact]
        public void UnitCodeIsInitializedCorrectly()
        {
            _testClass.UnitCode.Should().Be(_unitCode);
        }

        [Fact]
        public void AccessRightsIsInitializedCorrectly()
        {
            _testClass.AccessRights.Should().Be(_accessRights);
        }

        [Fact]
        public void ButtonValueIsInitializedCorrectly()
        {
            _testClass.ButtonValue.Should().Be(_buttonValue);
        }

        [Fact]
        public void DisplayFormatIsInitializedCorrectly()
        {
            _testClass.DisplayFormat.Should().Be(_displayFormat);
        }
    }
}