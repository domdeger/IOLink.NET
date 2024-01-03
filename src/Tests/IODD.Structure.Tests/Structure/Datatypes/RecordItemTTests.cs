namespace IODD.Structure.Tests.Datatypes
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Common;
    using IOLinkNET.IODD.Structure.Datatypes;

    using Xunit;

    public class RecordItemTTests
    {
        private readonly RecordItemT _testClass;
        private readonly byte _subindex;
        private readonly ushort _bitOffset;
        private readonly TextRefT _name;
        private readonly TextRefT _description;
        private readonly SimpleDatatypeT _type;
        private readonly DatatypeRefT _ref;

        public RecordItemTTests()
        {
            _subindex = (byte)11;
            _bitOffset = (ushort)22282;
            _name = new TextRefT("TestValue1167486296");
            _description = new TextRefT("TestValue995351579");
            _type = new UIntegerT("TestValue2035740349", (ushort)10107, new[] { new SingleValueT<uint>((uint)173410344, (uint)1052400654, new TextRefT("TestValue1268412090")), new SingleValueT<uint>((uint)745156145, (uint)663952633, new TextRefT("TestValue1482038731")), new SingleValueT<uint>((uint)194260514, (uint)29213942, new TextRefT("TestValue636262807")) }, new[] { new ValueRangeT<uint>((uint)1701255594, (uint)1693930241, new TextRefT("TestValue138358314")), new ValueRangeT<uint>((uint)1792723653, (uint)350931389, new TextRefT("TestValue486202366")), new ValueRangeT<uint>((uint)204561721, (uint)1441717887, new TextRefT("TestValue793294577")) });
            _ref = new DatatypeRefT("TestValue979595338");
            _testClass = new RecordItemT(_subindex, _bitOffset, _name, _description, _type, _ref);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new RecordItemT(_subindex, _bitOffset, _name, _description, _type, _ref);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_RecordItemT()
        {
            // Arrange
            var same = new RecordItemT(_subindex, _bitOffset, _name, _description, _type, _ref);
            var different = new RecordItemT((byte)118, (ushort)26002, new TextRefT("TestValue146539203"), new TextRefT("TestValue806181183"), new UIntegerT("TestValue692160851", (ushort)20759, new[] { new SingleValueT<uint>((uint)513642091, (uint)1225181893, new TextRefT("TestValue591922924")), new SingleValueT<uint>((uint)518529125, (uint)1653285771, new TextRefT("TestValue1136780338")), new SingleValueT<uint>((uint)1716687181, (uint)1953992663, new TextRefT("TestValue1969441956")) }, new[] { new ValueRangeT<uint>((uint)2047174764, (uint)1265583732, new TextRefT("TestValue382948279")), new ValueRangeT<uint>((uint)1749530728, (uint)1195107257, new TextRefT("TestValue1104253766")), new ValueRangeT<uint>((uint)172808118, (uint)1310720574, new TextRefT("TestValue2130559835")) }), new DatatypeRefT("TestValue291805292"));

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
        public void SubindexIsInitializedCorrectly()
        {
            _testClass.Subindex.Should().Be(_subindex);
        }

        [Fact]
        public void BitOffsetIsInitializedCorrectly()
        {
            _testClass.BitOffset.Should().Be(_bitOffset);
        }

        [Fact]
        public void NameIsInitializedCorrectly()
        {
            _testClass.Name.Should().BeSameAs(_name);
        }

        [Fact]
        public void DescriptionIsInitializedCorrectly()
        {
            _testClass.Description.Should().BeSameAs(_description);
        }

        [Fact]
        public void TypeIsInitializedCorrectly()
        {
            _testClass.Type.Should().BeSameAs(_type);
        }

        [Fact]
        public void RefIsInitializedCorrectly()
        {
            _testClass.Ref.Should().BeSameAs(_ref);
        }
    }
}