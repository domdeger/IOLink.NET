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

    public class UIVariableTests
    {
        private readonly UIVariable _testClass;
        private readonly string _variableId;
        private readonly VariableT _variable;
        private readonly decimal? _gradient;
        private readonly decimal? _offset;
        private readonly uint? _unitCode;
        private readonly AccessRightsT? _accessRights;
        private readonly string _buttonValue;
        private readonly DisplayFormat? _displayFormat;
        private readonly IODDPortReader _ioddPortReader;

        public UIVariableTests()
        {
            _variableId = "TestValue357966216";
            _variable = new VariableT("TestValue215575305", (ushort)33800, new UIntegerT("TestValue29661109", (ushort)56969, new[] { new SingleValueT<uint>((uint)1145714561, (uint)1965839200, new TextRefT("TestValue730022085")), new SingleValueT<uint>((uint)1188754989, (uint)845328606, new TextRefT("TestValue1453447765")), new SingleValueT<uint>((uint)899667524, (uint)606996001, new TextRefT("TestValue2039754301")) }, new[] { new ValueRangeT<uint>((uint)1025180914, (uint)900803418, new TextRefT("TestValue903402760")), new ValueRangeT<uint>((uint)1723092778, (uint)537817027, new TextRefT("TestValue1888061639")), new ValueRangeT<uint>((uint)1821154768, (uint)1547817914, new TextRefT("TestValue1553774230")) }), new DatatypeRefT("TestValue971986441"), new TextRefT("TestValue300745510"), new TextRefT("TestValue273564461"), AccessRightsT.ReadOnly, new[] { new RecordItemInfoT((byte)115, new object(), false, true), new RecordItemInfoT((byte)21, new object(), true, false), new RecordItemInfoT((byte)26, new object(), true, true) }, true, true, true);
            _gradient = 487556471.16M;
            _offset = 1033812040.14M;
            _unitCode = (uint)1411632636;
            _accessRights = new AccessRightsT?();
            _buttonValue = "TestValue50567228";
            _displayFormat = new DisplayFormat?();
            _ioddPortReader = new IODDPortReader(Substitute.For<IMasterConnection>(), Substitute.For<IDeviceDefinitionProvider>(), Substitute.For<IIoddDataConverter>(), Substitute.For<ITypeResolverFactory>());
            _testClass = new UIVariable(_variableId, _variable, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat, _ioddPortReader);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new UIVariable(_variableId, _variable, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat, _ioddPortReader);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_UIVariable()
        {
            // Arrange
            var same = new UIVariable(_variableId, _variable, _gradient, _offset, _unitCode, _accessRights, _buttonValue, _displayFormat, _ioddPortReader);
            var different = new UIVariable("TestValue451215640", new VariableT("TestValue882304160", (ushort)26429, new UIntegerT("TestValue1151854238", (ushort)2487, new[] { new SingleValueT<uint>((uint)1963186868, (uint)182528876, new TextRefT("TestValue1661876368")), new SingleValueT<uint>((uint)655886425, (uint)1913702651, new TextRefT("TestValue495969709")), new SingleValueT<uint>((uint)1036770659, (uint)957631949, new TextRefT("TestValue809284722")) }, new[] { new ValueRangeT<uint>((uint)135608840, (uint)1876399246, new TextRefT("TestValue1593232868")), new ValueRangeT<uint>((uint)1082075284, (uint)993183249, new TextRefT("TestValue629235805")), new ValueRangeT<uint>((uint)1768686835, (uint)668169089, new TextRefT("TestValue708079506")) }), new DatatypeRefT("TestValue1065954479"), new TextRefT("TestValue384357023"), new TextRefT("TestValue282674728"), AccessRightsT.WriteOnly, new[] { new RecordItemInfoT((byte)85, new object(), true, true), new RecordItemInfoT((byte)5, new object(), true, false), new RecordItemInfoT((byte)177, new object(), false, true) }, true, false, true), 1145383202.7M, 2045083640.49M, (uint)2053159080, new AccessRightsT?(), "TestValue1668756739", new DisplayFormat?(), new IODDPortReader(Substitute.For<IMasterConnection>(), Substitute.For<IDeviceDefinitionProvider>(), Substitute.For<IIoddDataConverter>(), Substitute.For<ITypeResolverFactory>()));

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