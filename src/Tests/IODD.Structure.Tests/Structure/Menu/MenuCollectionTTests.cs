namespace IODD.Structure.Tests.Structure.Menu
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.ProcessData;
    using IOLinkNET.IODD.Structure.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.Structure.Menu;

    using Xunit;

    public class MenuCollectionTTests
    {
        private readonly MenuCollectionT _testClass;
        private readonly MenuT _menu;

        public MenuCollectionTTests()
        {
            _menu = new MenuT("TestValue1633807788", "TestValue1511326098", new[] { new UIVariableRefT("TestValue1576617592", 1283472484.47M, 1390728639.96M, (uint)1411489877, new AccessRightsT?(), "TestValue1976203037", new DisplayFormat?()), new UIVariableRefT("TestValue1904894038", 699424530.75M, 2077829299.26M, (uint)1118100691, new AccessRightsT?(), "TestValue1376033965", new DisplayFormat?()), new UIVariableRefT("TestValue1085904896", 1141772840.51M, 872364407.31M, (uint)1026278874, new AccessRightsT?(), "TestValue1291584438", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue1194985119", new ConditionT("TestValue2060192335", (byte)210, 1314697545)), new UIMenuRefT("TestValue33341932", new ConditionT("TestValue1772732547", (byte)95, 2016232910)), new UIMenuRefT("TestValue1735476855", new ConditionT("TestValue1454641458", (byte)227, 1133337098)) }, new[] { new UIRecordItemRefT("TestValue1328381366", (byte)250, 1649082933.63M, 365085482.85M, (uint)474404436, new AccessRightsT?(), "TestValue1276749731", new DisplayFormat?()), new UIRecordItemRefT("TestValue1306083334", (byte)219, 476440746.21M, 1985052815.46M, (uint)1226545576, new AccessRightsT?(), "TestValue530500005", new DisplayFormat?()), new UIRecordItemRefT("TestValue1384022042", (byte)167, 1607750512.83M, 35632484.91M, (uint)874905082, new AccessRightsT?(), "TestValue592584793", new DisplayFormat?()) });
            _testClass = new MenuCollectionT(_menu);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new MenuCollectionT(_menu);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_MenuCollectionT()
        {
            // Arrange
            var same = new MenuCollectionT(_menu);
            var different = new MenuCollectionT(new MenuT("TestValue1869283873", "TestValue1627687156", new[] { new UIVariableRefT("TestValue130807220", 432745128.09M, 117328278.87M, (uint)457170078, new AccessRightsT?(), "TestValue1879438586", new DisplayFormat?()), new UIVariableRefT("TestValue561917731", 1584461197.44M, 1867216426.02M, (uint)770354314, new AccessRightsT?(), "TestValue1427687590", new DisplayFormat?()), new UIVariableRefT("TestValue1816783196", 2083288745.34M, 56483719.38M, (uint)771556940, new AccessRightsT?(), "TestValue1525591706", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue2134348812", new ConditionT("TestValue1599844001", (byte)119, 611500460)), new UIMenuRefT("TestValue2024039815", new ConditionT("TestValue1722576482", (byte)157, 810331159)), new UIMenuRefT("TestValue1337364912", new ConditionT("TestValue833646759", (byte)147, 2078159150)) }, new[] { new UIRecordItemRefT("TestValue773614264", (byte)17, 762568160.31M, 1129914549.72M, (uint)1142892062, new AccessRightsT?(), "TestValue1732841232", new DisplayFormat?()), new UIRecordItemRefT("TestValue291461619", (byte)119, 882352424.25M, 1973504507.04M, (uint)1208570259, new AccessRightsT?(), "TestValue636818276", new DisplayFormat?()), new UIRecordItemRefT("TestValue2059970352", (byte)216, 1551814885.17M, 1063649038.32M, (uint)1201049336, new AccessRightsT?(), "TestValue188320658", new DisplayFormat?()) }));

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
        public void MenuIsInitializedCorrectly()
        {
            _testClass.Menu.Should().BeSameAs(_menu);
        }
    }
}