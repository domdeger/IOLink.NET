namespace IODD.Structure.Tests.ProcessData
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Common;
    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.ProcessData;

    using Xunit;

    public class ProcessDataTTests
    {
        private readonly ProcessDataT _testClass;
        private readonly ConditionT _condition;
        private readonly ProcessDataItemT _processDataIn;
        private readonly ProcessDataItemT _processDataOut;

        public ProcessDataTTests()
        {
            _condition = new ConditionT("TestValue588861177", (byte)203, 1331457517);
            _processDataIn = new ProcessDataItemT(new UIntegerT("TestValue701333771", (ushort)1141, new[] { new SingleValueT<uint>((uint)790171458, (uint)346325933, new TextRefT("TestValue465661968")), new SingleValueT<uint>((uint)972387165, (uint)1327504782, new TextRefT("TestValue1319484222")), new SingleValueT<uint>((uint)1530931221, (uint)1278696267, new TextRefT("TestValue2014317768")) }, new[] { new ValueRangeT<uint>((uint)707838997, (uint)1527768595, new TextRefT("TestValue889464582")), new ValueRangeT<uint>((uint)1700143515, (uint)914557575, new TextRefT("TestValue84127130")), new ValueRangeT<uint>((uint)428057752, (uint)2145335618, new TextRefT("TestValue1295030272")) }), new DatatypeRefT("TestValue433470477"), "TestValue625628747", (ushort)42120);
            _processDataOut = new ProcessDataItemT(new UIntegerT("TestValue1830318458", (ushort)34055, new[] { new SingleValueT<uint>((uint)1058109428, (uint)617861460, new TextRefT("TestValue727428541")), new SingleValueT<uint>((uint)864629896, (uint)934896347, new TextRefT("TestValue2007124977")), new SingleValueT<uint>((uint)220754161, (uint)1357589260, new TextRefT("TestValue1073371891")) }, new[] { new ValueRangeT<uint>((uint)1288407579, (uint)1456355443, new TextRefT("TestValue1583848102")), new ValueRangeT<uint>((uint)1878649741, (uint)1197917404, new TextRefT("TestValue1005756451")), new ValueRangeT<uint>((uint)2122900392, (uint)1451287970, new TextRefT("TestValue172993115")) }), new DatatypeRefT("TestValue916484896"), "TestValue875796289", (ushort)47439);
            _testClass = new ProcessDataT(_condition, _processDataIn, _processDataOut);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new ProcessDataT(_condition, _processDataIn, _processDataOut);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_ProcessDataT()
        {
            // Arrange
            var same = new ProcessDataT(_condition, _processDataIn, _processDataOut);
            var different = new ProcessDataT(new ConditionT("TestValue2142177396", (byte)23, 726257326), new ProcessDataItemT(new UIntegerT("TestValue1654206453", (ushort)14784, new[] { new SingleValueT<uint>((uint)1633519731, (uint)591009206, new TextRefT("TestValue420535344")), new SingleValueT<uint>((uint)897987040, (uint)75705024, new TextRefT("TestValue804636498")), new SingleValueT<uint>((uint)1107336647, (uint)1377853515, new TextRefT("TestValue1555036187")) }, new[] { new ValueRangeT<uint>((uint)354525705, (uint)600076241, new TextRefT("TestValue454854326")), new ValueRangeT<uint>((uint)596034874, (uint)1419054937, new TextRefT("TestValue1793563607")), new ValueRangeT<uint>((uint)1497733384, (uint)454396704, new TextRefT("TestValue1748540650")) }), new DatatypeRefT("TestValue243788072"), "TestValue1478193120", (ushort)10771), new ProcessDataItemT(new UIntegerT("TestValue1377623995", (ushort)34776, new[] { new SingleValueT<uint>((uint)1319613527, (uint)1129666154, new TextRefT("TestValue452635632")), new SingleValueT<uint>((uint)463754651, (uint)954522169, new TextRefT("TestValue1708910701")), new SingleValueT<uint>((uint)1063415679, (uint)422833426, new TextRefT("TestValue1171215")) }, new[] { new ValueRangeT<uint>((uint)1357907090, (uint)450436902, new TextRefT("TestValue373605246")), new ValueRangeT<uint>((uint)1777228602, (uint)937053916, new TextRefT("TestValue175384851")), new ValueRangeT<uint>((uint)1212702555, (uint)651718945, new TextRefT("TestValue476511455")) }), new DatatypeRefT("TestValue500796226"), "TestValue1790364864", (ushort)19873));

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
        public void ConditionIsInitializedCorrectly()
        {
            _testClass.Condition.Should().BeSameAs(_condition);
        }

        [Fact]
        public void ProcessDataInIsInitializedCorrectly()
        {
            _testClass.ProcessDataIn.Should().BeSameAs(_processDataIn);
        }

        [Fact]
        public void ProcessDataOutIsInitializedCorrectly()
        {
            _testClass.ProcessDataOut.Should().BeSameAs(_processDataOut);
        }
    }
}