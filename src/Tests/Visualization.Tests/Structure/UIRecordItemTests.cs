namespace Visualization.Tests.Structure
{
    using FluentAssertions;

    using IOLinkNET.Conversion;
    using IOLinkNET.Device.Contract;
    using IOLinkNET.Integration;
    using IOLinkNET.IODD.Provider;
    using IOLinkNET.IODD.Resolution.Contracts;
    using IOLinkNET.IODD.Structure.Common;
    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.DeviceFunction;
    using IOLinkNET.IODD.Structure.Structure.Datatypes;
    using IOLinkNET.Visualization.Structure.Structure;

    using NSubstitute;

    using Xunit;

    public class UIRecordItemTests
    {
        private readonly UIRecordItem _testClass;
        private readonly string _variableId;
        private readonly VariableT _variable;
        private readonly byte _subIndex;
        private readonly decimal? _gradient;
        private readonly decimal? _offset;
        private readonly uint? _unitCode;
        private readonly AccessRightsT? _accessRights;
        private readonly string _buttonValue;
        private readonly DisplayFormat? _displayFormat;
        private readonly IODDPortReader _ioddPortReader;

        public UIRecordItemTests()
        {
            _variableId = "TestValue1105879107";
            _variable = new VariableT("TestValue1703891405", (ushort)15476, new UIntegerT("TestValue1036374404", (ushort)61599, new[] { new SingleValueT<uint>((uint)1051325625, (uint)158837660, new TextRefT("TestValue1911265484")), new SingleValueT<uint>((uint)611025009, (uint)1381969546, new TextRefT("TestValue909996578")), new SingleValueT<uint>((uint)1311364733, (uint)965184842, new TextRefT("TestValue437614271")) }, new[] { new ValueRangeT<uint>((uint)1405917021, (uint)700849314, new TextRefT("TestValue2146993631")), new ValueRangeT<uint>((uint)673843489, (uint)347022131, new TextRefT("TestValue1908311733")), new ValueRangeT<uint>((uint)118457194, (uint)403044278, new TextRefT("TestValue543636686")) }), new DatatypeRefT("TestValue588167357"), new TextRefT("TestValue1151443351"), new TextRefT("TestValue1981093896"), AccessRightsT.ReadWrite, new[] { new RecordItemInfoT((byte)133, new object(), true, false), new RecordItemInfoT((byte)39, new object(), false, true), new RecordItemInfoT((byte)118, new object(), false, true) }, true, false, true);
            _subIndex = (byte)193;
            _gradient = 1894999252.41M;
            _offset = 1016352437.76M;
            _unitCode = (uint)1253983679;
            _accessRights = new AccessRightsT?();
            _buttonValue = "TestValue355368408";
            _displayFormat = new DisplayFormat?();
            _ioddPortReader = new IODDPortReader(Substitute.For<IMasterConnection>(), Substitute.For<IDeviceDefinitionProvider>(), Substitute.For<IIoddDataConverter>(), Substitute.For<ITypeResolverFactory>());
            _testClass = new UIRecordItem(_variableId, _variable, _subIndex, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat, _ioddPortReader);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new UIRecordItem(_variableId, _variable, _subIndex, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat, _ioddPortReader);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_UIRecordItem()
        {
            // Arrange
            var same = new UIRecordItem(_variableId, _variable, _subIndex, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat, _ioddPortReader);
            var different = new UIRecordItem("TestValue969928270", new VariableT("TestValue1540076759", (ushort)61257, new UIntegerT("TestValue544385051", (ushort)1186, new[] { new SingleValueT<uint>((uint)1655606368, (uint)2118358027, new TextRefT("TestValue28529225")), new SingleValueT<uint>((uint)603188131, (uint)1815325842, new TextRefT("TestValue639656484")), new SingleValueT<uint>((uint)702834829, (uint)1160254719, new TextRefT("TestValue2066468244")) }, new[] { new ValueRangeT<uint>((uint)2032414700, (uint)37416730, new TextRefT("TestValue1813540086")), new ValueRangeT<uint>((uint)1184118574, (uint)1985347282, new TextRefT("TestValue408655073")), new ValueRangeT<uint>((uint)1049404290, (uint)70260746, new TextRefT("TestValue933080758")) }), new DatatypeRefT("TestValue2115045789"), new TextRefT("TestValue1666687784"), new TextRefT("TestValue1285550668"), AccessRightsT.ReadWrite, new[] { new RecordItemInfoT((byte)46, new object(), true, true), new RecordItemInfoT((byte)254, new object(), true, false), new RecordItemInfoT((byte)22, new object(), false, true) }, false, true, false), (byte)49, 229180600.44M, 300995670.69M, (uint)236547701, new AccessRightsT?(), "TestValue710441780", new DisplayFormat?(), new IODDPortReader(Substitute.For<IMasterConnection>(), Substitute.For<IDeviceDefinitionProvider>(), Substitute.For<IIoddDataConverter>(), Substitute.For<ITypeResolverFactory>()));

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
        public void VariableIsInitializedCorrectly()
        {
            _testClass.Variable.Should().BeSameAs(_variable);
        }

        [Fact]
        public void SubIndexIsInitializedCorrectly()
        {
            _testClass.SubIndex.Should().Be(_subIndex);
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

        [Fact]
        public void IoddPortReaderIsInitializedCorrectly()
        {
            _testClass.IoddPortReader.Should().BeSameAs(_ioddPortReader);
        }
    }
}