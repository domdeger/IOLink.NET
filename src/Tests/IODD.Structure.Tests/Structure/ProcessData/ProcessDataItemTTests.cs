namespace IODD.Structure.Tests.ProcessData
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Common;
    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.ProcessData;

    using Xunit;

    public class ProcessDataItemTTests
    {
        private readonly ProcessDataItemT _testClass;
        private readonly DatatypeT _datatype;
        private readonly DatatypeRefT _ref;
        private readonly string _id;
        private readonly ushort _bitLength;

        public ProcessDataItemTTests()
        {
            _datatype = new UIntegerT("TestValue186366870", (ushort)13974, new[] { new SingleValueT<uint>((uint)886930496, (uint)1052431433, new TextRefT("TestValue676449459")), new SingleValueT<uint>((uint)1633510226, (uint)1785114356, new TextRefT("TestValue731722285")), new SingleValueT<uint>((uint)1046407709, (uint)1141093488, new TextRefT("TestValue161619157")) }, new[] { new ValueRangeT<uint>((uint)2091637124, (uint)1975995587, new TextRefT("TestValue450468152")), new ValueRangeT<uint>((uint)1056759022, (uint)182069436, new TextRefT("TestValue1427937063")), new ValueRangeT<uint>((uint)425465221, (uint)1230840426, new TextRefT("TestValue433088125")) });
            _ref = new DatatypeRefT("TestValue1702354076");
            _id = "TestValue1006345735";
            _bitLength = (ushort)39242;
            _testClass = new ProcessDataItemT(_datatype, _ref, _id, _bitLength);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ProcessDataItemT(_datatype, _ref, _id, _bitLength);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_ProcessDataItemT()
        {
            // Arrange
            var same = new ProcessDataItemT(_datatype, _ref, _id, _bitLength);
            var different = new ProcessDataItemT(new UIntegerT("TestValue1503482922", (ushort)58630, new[] { new SingleValueT<uint>((uint)809615338, (uint)925671229, new TextRefT("TestValue257345761")), new SingleValueT<uint>((uint)1989741350, (uint)1493747841, new TextRefT("TestValue226535913")), new SingleValueT<uint>((uint)77492027, (uint)1663579372, new TextRefT("TestValue1978143616")) }, new[] { new ValueRangeT<uint>((uint)1302921527, (uint)623288545, new TextRefT("TestValue1703924336")), new ValueRangeT<uint>((uint)47697516, (uint)742630410, new TextRefT("TestValue114884361")), new ValueRangeT<uint>((uint)1522462344, (uint)1084492616, new TextRefT("TestValue278917194")) }), new DatatypeRefT("TestValue421280381"), "TestValue568586575", (ushort)62913);

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
        public void CanGetType()
        {
            // Assert
            _testClass.Type.Should().BeAssignableTo<DatatypeT>();
        }

        [Fact]
        public void DatatypeIsInitializedCorrectly()
        {
            _testClass.Datatype.Should().BeSameAs(_datatype);
        }

        [Fact]
        public void RefIsInitializedCorrectly()
        {
            _testClass.Ref.Should().BeSameAs(_ref);
        }

        [Fact]
        public void IdIsInitializedCorrectly()
        {
            _testClass.Id.Should().Be(_id);
        }

        [Fact]
        public void BitLengthIsInitializedCorrectly()
        {
            _testClass.BitLength.Should().Be(_bitLength);
        }
    }
}