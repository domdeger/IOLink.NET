namespace IODD.Structure.Tests.Structure.Menu
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.ProcessData;
    using IOLinkNET.IODD.Structure.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.Structure.Menu;

    using Xunit;

    public class UIMenuRefSimpleTTests
    {
        private readonly UIMenuRefSimpleT _testClass;
        private readonly string _menuId;
        private readonly MenuT _menu;

        public UIMenuRefSimpleTTests()
        {
            _menuId = "TestValue1335539345";
            _menu = new MenuT("TestValue899008963", "TestValue1462938183", new[] { new UIVariableRefT("TestValue314109537", 1389274415.1M, 1844917870.95M, (uint)1784793701, new AccessRightsT?(), "TestValue1124344313", new DisplayFormat?()), new UIVariableRefT("TestValue1085947570", 99569654.91M, 1574677637.28M, (uint)1710184505, new AccessRightsT?(), "TestValue1458340138", new DisplayFormat?()), new UIVariableRefT("TestValue533642999", 1570877888.58M, 1173439378.98M, (uint)864995679, new AccessRightsT?(), "TestValue1544404286", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue220539551", new ConditionT("TestValue643169500", (byte)188, 1118292737)), new UIMenuRefT("TestValue295206343", new ConditionT("TestValue1652115160", (byte)9, 2041947309)), new UIMenuRefT("TestValue17641579", new ConditionT("TestValue872095579", (byte)102, 741499946)) }, new[] { new UIRecordItemRefT("TestValue349716132", (byte)144, 545958422.46M, 480352588.65M, (uint)1996712734, new AccessRightsT?(), "TestValue1830791075", new DisplayFormat?()), new UIRecordItemRefT("TestValue755044373", (byte)86, 86150880.09M, 504921615.66M, (uint)207387067, new AccessRightsT?(), "TestValue1916092464", new DisplayFormat?()), new UIRecordItemRefT("TestValue759293442", (byte)12, 1841243835.42M, 91502829.99M, (uint)1416506688, new AccessRightsT?(), "TestValue1784542861", new DisplayFormat?()) });
            _testClass = new UIMenuRefSimpleT(_menuId, _menu);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new UIMenuRefSimpleT(_menuId, _menu);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_UIMenuRefSimpleT()
        {
            // Arrange
            var same = new UIMenuRefSimpleT(_menuId, _menu);
            var different = new UIMenuRefSimpleT("TestValue1175034136", new MenuT("TestValue31158869", "TestValue1295802415", new[] { new UIVariableRefT("TestValue1126114449", 1025484193.8M, 1978174738.98M, (uint)398999628, new AccessRightsT?(), "TestValue217246608", new DisplayFormat?()), new UIVariableRefT("TestValue603802620", 1938723603.3M, 228895587.36M, (uint)1508843828, new AccessRightsT?(), "TestValue1845911826", new DisplayFormat?()), new UIVariableRefT("TestValue912698122", 258819737.22M, 341003147.76M, (uint)1898342924, new AccessRightsT?(), "TestValue370553861", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue1158711351", new ConditionT("TestValue973135503", (byte)81, 1903437914)), new UIMenuRefT("TestValue430247929", new ConditionT("TestValue135709362", (byte)173, 1858001364)), new UIMenuRefT("TestValue435782433", new ConditionT("TestValue1816277543", (byte)42, 186429503)) }, new[] { new UIRecordItemRefT("TestValue1939756549", (byte)253, 619186214.79M, 376776541.35M, (uint)1844545090, new AccessRightsT?(), "TestValue831751366", new DisplayFormat?()), new UIRecordItemRefT("TestValue1593181178", (byte)162, 182345121.09M, 693792494.51M, (uint)86205007, new AccessRightsT?(), "TestValue1779466126", new DisplayFormat?()), new UIRecordItemRefT("TestValue1226988455", (byte)112, 493097866.47M, 718404300.9M, (uint)811593655, new AccessRightsT?(), "TestValue1442172592", new DisplayFormat?()) }));

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
        public void MenuIdIsInitializedCorrectly()
        {
            _testClass.MenuId.Should().Be(_menuId);
        }

        [Fact]
        public void MenuIsInitializedCorrectly()
        {
            _testClass.Menu.Should().BeSameAs(_menu);
        }
    }
}