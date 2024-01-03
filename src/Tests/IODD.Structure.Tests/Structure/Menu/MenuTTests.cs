namespace IODD.Structure.Tests.Structure.Menu
{
    using System.Collections.Generic;

    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.ProcessData;
    using IOLinkNET.IODD.Structure.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.Structure.Menu;

    using Xunit;

    public class MenuTTests
    {
        private readonly MenuT _testClass;
        private readonly string _id;
        private readonly string _name;
        private readonly IEnumerable<UIVariableRefT> _variableRefs;
        private readonly IEnumerable<UIMenuRefT> _menuRefs;
        private readonly IEnumerable<UIRecordItemRefT> _recordItemRefs;

        public MenuTTests()
        {
            _id = "TestValue731749949";
            _name = "TestValue1440605193";
            _variableRefs = new[] { new UIVariableRefT("TestValue1889456776", 945752660.82M, 824431519.89M, (uint)995941893, new AccessRightsT?(), "TestValue1171510695", new DisplayFormat?()), new UIVariableRefT("TestValue68097265", 2089548512.37M, 1170122224.59M, (uint)1437758140, new AccessRightsT?(), "TestValue1661895101", new DisplayFormat?()), new UIVariableRefT("TestValue257442508", 1120320067.68M, 2002289702.49M, (uint)912675093, new AccessRightsT?(), "TestValue515317630", new DisplayFormat?()) };
            _menuRefs = new[] { new UIMenuRefT("TestValue1863445180", new ConditionT("TestValue377694740", (byte)197, 805381277)), new UIMenuRefT("TestValue721446710", new ConditionT("TestValue50808933", (byte)126, 432425018)), new UIMenuRefT("TestValue1002402387", new ConditionT("TestValue1025818865", (byte)121, 2064926700)) };
            _recordItemRefs = new[] { new UIRecordItemRefT("TestValue1637969163", (byte)187, 796242499.47M, 694155116.16M, (uint)1031048375, new AccessRightsT?(), "TestValue1857906436", new DisplayFormat?()), new UIRecordItemRefT("TestValue266732702", (byte)137, 1660447720.8M, 148141186.38M, (uint)51899292, new AccessRightsT?(), "TestValue977060219", new DisplayFormat?()), new UIRecordItemRefT("TestValue1132182120", (byte)84, 20888005.05M, 712263466.53M, (uint)1042039950, new AccessRightsT?(), "TestValue1048414754", new DisplayFormat?()) };
            _testClass = new MenuT(_id, _name, _variableRefs, _menuRefs, _recordItemRefs);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new MenuT(_id, _name, _variableRefs, _menuRefs, _recordItemRefs);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_MenuT()
        {
            // Arrange
            var same = new MenuT(_id, _name, _variableRefs, _menuRefs, _recordItemRefs);
            var different = new MenuT("TestValue116238422", "TestValue422947286", new[] { new UIVariableRefT("TestValue1381599667", 364995075.06M, 454372739.37M, (uint)816905315, new AccessRightsT?(), "TestValue389157449", new DisplayFormat?()), new UIVariableRefT("TestValue997531653", 10200206.61M, 1375898297.4M, (uint)828136726, new AccessRightsT?(), "TestValue522880700", new DisplayFormat?()), new UIVariableRefT("TestValue1977840371", 2096430608.25M, 147092698.17M, (uint)150654212, new AccessRightsT?(), "TestValue472685900", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue1752660257", new ConditionT("TestValue633472787", (byte)114, 1373877780)), new UIMenuRefT("TestValue1421213643", new ConditionT("TestValue1755782149", (byte)226, 985581357)), new UIMenuRefT("TestValue1713807618", new ConditionT("TestValue325795448", (byte)81, 1820682804)) }, new[] { new UIRecordItemRefT("TestValue10365044", (byte)3, 338443382.97M, 1522490027.85M, (uint)2101471280, new AccessRightsT?(), "TestValue909580443", new DisplayFormat?()), new UIRecordItemRefT("TestValue599984926", (byte)81, 1256594396.31M, 1106624641.32M, (uint)2134863685, new AccessRightsT?(), "TestValue312009335", new DisplayFormat?()), new UIRecordItemRefT("TestValue33516722", (byte)219, 1014175888.11M, 322868292.12M, (uint)1154339220, new AccessRightsT?(), "TestValue319280838", new DisplayFormat?()) });

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
        public void IdIsInitializedCorrectly()
        {
            _testClass.Id.Should().Be(_id);
        }

        [Fact]
        public void NameIsInitializedCorrectly()
        {
            _testClass.Name.Should().Be(_name);
        }

        [Fact]
        public void VariableRefsIsInitializedCorrectly()
        {
            _testClass.VariableRefs.Should().BeSameAs(_variableRefs);
        }

        [Fact]
        public void MenuRefsIsInitializedCorrectly()
        {
            _testClass.MenuRefs.Should().BeSameAs(_menuRefs);
        }

        [Fact]
        public void RecordItemRefsIsInitializedCorrectly()
        {
            _testClass.RecordItemRefs.Should().BeSameAs(_recordItemRefs);
        }
    }
}