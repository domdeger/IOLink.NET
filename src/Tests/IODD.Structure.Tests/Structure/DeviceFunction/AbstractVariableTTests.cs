namespace IODD.Structure.Tests.DeviceFunction
{
    using System.Collections.Generic;

    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Common;
    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.DeviceFunction;

    using Xunit;

    public class AbstractVariableTTests
    {
        private readonly AbstractVariableT _testClass;
        private readonly DatatypeT _type;
        private readonly DatatypeRefT _ref;
        private readonly TextRefT _name;
        private readonly TextRefT _description;
        private readonly AccessRightsT _accessRights;
        private readonly IEnumerable<RecordItemInfoT> _recordItemInfos;
        private readonly bool _dynamic;
        private readonly bool _modifiesOtherVariables;
        private readonly bool _excludedFromDataStorage;

        public AbstractVariableTTests()
        {
            _type = new UIntegerT("TestValue2061644533", (ushort)50024, new[] { new SingleValueT<uint>((uint)1741822291, (uint)1162601318, new TextRefT("TestValue338635272")), new SingleValueT<uint>((uint)374651218, (uint)1034632808, new TextRefT("TestValue918493055")), new SingleValueT<uint>((uint)823894844, (uint)1095827210, new TextRefT("TestValue968110841")) }, new[] { new ValueRangeT<uint>((uint)2124123951, (uint)689455984, new TextRefT("TestValue615373209")), new ValueRangeT<uint>((uint)2112837931, (uint)897503864, new TextRefT("TestValue1148459421")), new ValueRangeT<uint>((uint)1482609588, (uint)1438139641, new TextRefT("TestValue547958773")) });
            _ref = new DatatypeRefT("TestValue261370496");
            _name = new TextRefT("TestValue1049254530");
            _description = new TextRefT("TestValue1912049671");
            _accessRights = AccessRightsT.ReadOnly;
            _recordItemInfos = new[] { new RecordItemInfoT((byte)3, new object(), false, true), new RecordItemInfoT((byte)113, new object(), false, false), new RecordItemInfoT((byte)182, new object(), false, true) };
            _dynamic = false;
            _modifiesOtherVariables = true;
            _excludedFromDataStorage = true;
            _testClass = new AbstractVariableT(_type, _ref, _name, _description, _accessRights, _recordItemInfos, _dynamic, _modifiesOtherVariables, _excludedFromDataStorage);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new AbstractVariableT(_type, _ref, _name, _description, _accessRights, _recordItemInfos, _dynamic, _modifiesOtherVariables, _excludedFromDataStorage);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_AbstractVariableT()
        {
            // Arrange
            var same = new AbstractVariableT(_type, _ref, _name, _description, _accessRights, _recordItemInfos, _dynamic, _modifiesOtherVariables, _excludedFromDataStorage);
            var different = new AbstractVariableT(new UIntegerT("TestValue1088168300", (ushort)23707, new[] { new SingleValueT<uint>((uint)1957736119, (uint)1171867521, new TextRefT("TestValue1442604847")), new SingleValueT<uint>((uint)2033104853, (uint)109965988, new TextRefT("TestValue207890595")), new SingleValueT<uint>((uint)633722946, (uint)1386454235, new TextRefT("TestValue1439448211")) }, new[] { new ValueRangeT<uint>((uint)1306533402, (uint)1245653488, new TextRefT("TestValue428761810")), new ValueRangeT<uint>((uint)607574612, (uint)1829140103, new TextRefT("TestValue830827233")), new ValueRangeT<uint>((uint)947603291, (uint)528926740, new TextRefT("TestValue1012390003")) }), new DatatypeRefT("TestValue1874659815"), new TextRefT("TestValue1379729857"), new TextRefT("TestValue1135416978"), AccessRightsT.WriteOnly, new[] { new RecordItemInfoT((byte)225, new object(), false, false), new RecordItemInfoT((byte)177, new object(), false, true), new RecordItemInfoT((byte)159, new object(), false, true) }, false, true, true);

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
        public void TypeIsInitializedCorrectly()
        {
            _testClass.Type.Should().BeSameAs(_type);
        }

        [Fact]
        public void RefIsInitializedCorrectly()
        {
            _testClass.Ref.Should().BeSameAs(_ref);
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
        public void AccessRightsIsInitializedCorrectly()
        {
            _testClass.AccessRights.Should().Be(_accessRights);
        }

        [Fact]
        public void RecordItemInfosIsInitializedCorrectly()
        {
            _testClass.RecordItemInfos.Should().BeSameAs(_recordItemInfos);
        }

        [Fact]
        public void DynamicIsInitializedCorrectly()
        {
            _testClass.Dynamic.Should().Be(_dynamic);
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