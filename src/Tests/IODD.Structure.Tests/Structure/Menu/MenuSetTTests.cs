namespace IODD.Structure.Tests.Structure.Menu
{
    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.ProcessData;
    using IOLinkNET.IODD.Structure.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.Structure.Menu;

    using Xunit;

    public class MenuSetTTests
    {
        private readonly MenuSetT _testClass;
        private readonly UIMenuRefSimpleT _identificationMenu;
        private readonly UIMenuRefSimpleT _parameterMenu;
        private readonly UIMenuRefSimpleT _observationMenu;
        private readonly UIMenuRefSimpleT _diagnosisMenu;

        public MenuSetTTests()
        {
            _identificationMenu = new UIMenuRefSimpleT("TestValue1637125252", new MenuT("TestValue583632055", "TestValue2015127979", new[] { new UIVariableRefT("TestValue2128567980", 1972800427.95M, 2032068377.79M, (uint)949275628, new AccessRightsT?(), "TestValue1774268779", new DisplayFormat?()), new UIVariableRefT("TestValue928632148", 1753296896.34M, 629562548.34M, (uint)631242228, new AccessRightsT?(), "TestValue826851592", new DisplayFormat?()), new UIVariableRefT("TestValue1737335068", 82610.55M, 283496164.38M, (uint)673891134, new AccessRightsT?(), "TestValue371490834", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue1913076290", new ConditionT("TestValue1916510709", (byte)75, 140909916)), new UIMenuRefT("TestValue391273742", new ConditionT("TestValue370442474", (byte)83, 200272293)), new UIMenuRefT("TestValue155086699", new ConditionT("TestValue254622271", (byte)208, 1149044254)) }, new[] { new UIRecordItemRefT("TestValue1711433249", (byte)91, 1451037107.52M, 883074708.45M, (uint)659100525, new AccessRightsT?(), "TestValue186638814", new DisplayFormat?()), new UIRecordItemRefT("TestValue1273683496", (byte)137, 1868721202.26M, 382487435.55M, (uint)53254250, new AccessRightsT?(), "TestValue1264479943", new DisplayFormat?()), new UIRecordItemRefT("TestValue1064806748", (byte)188, 887544.9M, 1414440842.76M, (uint)996915930, new AccessRightsT?(), "TestValue1977638362", new DisplayFormat?()) }));
            _parameterMenu = new UIMenuRefSimpleT("TestValue1074309523", new MenuT("TestValue914689574", "TestValue1661913171", new[] { new UIVariableRefT("TestValue898206323", 700774569.5M, 835003659.6M, (uint)1394776625, new AccessRightsT?(), "TestValue1496215336", new DisplayFormat?()), new UIVariableRefT("TestValue192358313", 1628238649.95M, 1415258682.75M, (uint)1792455412, new AccessRightsT?(), "TestValue1897507622", new DisplayFormat?()), new UIVariableRefT("TestValue694653357", 17328275.91M, 1907800825.59M, (uint)59573717, new AccessRightsT?(), "TestValue2013334336", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue1313031827", new ConditionT("TestValue2082340584", (byte)128, 1960928278)), new UIMenuRefT("TestValue1160159913", new ConditionT("TestValue1659567052", (byte)74, 1526725345)), new UIMenuRefT("TestValue1863256459", new ConditionT("TestValue1517330335", (byte)145, 953710449)) }, new[] { new UIRecordItemRefT("TestValue369545964", (byte)168, 1337331609.9M, 321682664.16M, (uint)1327796395, new AccessRightsT?(), "TestValue842075896", new DisplayFormat?()), new UIRecordItemRefT("TestValue1634614730", (byte)96, 61595797.23M, 616033447.92M, (uint)1644701677, new AccessRightsT?(), "TestValue1310368836", new DisplayFormat?()), new UIRecordItemRefT("TestValue2141764148", (byte)210, 1860939779.49M, 94190344.38M, (uint)636326970, new AccessRightsT?(), "TestValue1506084540", new DisplayFormat?()) }));
            _observationMenu = new UIMenuRefSimpleT("TestValue1246976634", new MenuT("TestValue1285218854", "TestValue1525473223", new[] { new UIVariableRefT("TestValue135045821", 114539334.03M, 1051438403.07M, (uint)899403819, new AccessRightsT?(), "TestValue1260864892", new DisplayFormat?()), new UIVariableRefT("TestValue1902013308", 2322657.81M, 264160725.84M, (uint)1328611402, new AccessRightsT?(), "TestValue1127665228", new DisplayFormat?()), new UIVariableRefT("TestValue2024929937", 269902235.79M, 1372270195.89M, (uint)1275139541, new AccessRightsT?(), "TestValue11784947", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue441615402", new ConditionT("TestValue1572575638", (byte)179, 1322911060)), new UIMenuRefT("TestValue292456811", new ConditionT("TestValue1393830438", (byte)231, 690775819)), new UIMenuRefT("TestValue437638907", new ConditionT("TestValue1915349354", (byte)233, 1531161922)) }, new[] { new UIRecordItemRefT("TestValue1927313548", (byte)63, 881494391.25M, 353600199.81M, (uint)270353701, new AccessRightsT?(), "TestValue2085851608", new DisplayFormat?()), new UIRecordItemRefT("TestValue1575720873", (byte)27, 1289052251.19M, 285893206.83M, (uint)1573011812, new AccessRightsT?(), "TestValue66931503", new DisplayFormat?()), new UIRecordItemRefT("TestValue1087546235", (byte)193, 540933930.9M, 872279319.78M, (uint)1642074427, new AccessRightsT?(), "TestValue1767255387", new DisplayFormat?()) }));
            _diagnosisMenu = new UIMenuRefSimpleT("TestValue1037740315", new MenuT("TestValue755632637", "TestValue501342097", new[] { new UIVariableRefT("TestValue1867952204", 1782999906.93M, 1199122629.21M, (uint)2139227578, new AccessRightsT?(), "TestValue2071549221", new DisplayFormat?()), new UIVariableRefT("TestValue992762043", 130326357.15M, 328098977.91M, (uint)1572404125, new AccessRightsT?(), "TestValue624420086", new DisplayFormat?()), new UIVariableRefT("TestValue1131538112", 1427283754.38M, 367142872.14M, (uint)222516218, new AccessRightsT?(), "TestValue1878077118", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue438213027", new ConditionT("TestValue770493309", (byte)208, 334260560)), new UIMenuRefT("TestValue1957894285", new ConditionT("TestValue1040639398", (byte)101, 152834385)), new UIMenuRefT("TestValue2147047473", new ConditionT("TestValue1447409106", (byte)27, 807671847)) }, new[] { new UIRecordItemRefT("TestValue847432528", (byte)127, 1184223188.61M, 809688495.33M, (uint)877609039, new AccessRightsT?(), "TestValue1211015140", new DisplayFormat?()), new UIRecordItemRefT("TestValue1029819825", (byte)7, 873882093.15M, 1808380572.57M, (uint)365427988, new AccessRightsT?(), "TestValue346288127", new DisplayFormat?()), new UIRecordItemRefT("TestValue1093089565", (byte)171, 2030064974.19M, 1858380977.97M, (uint)1811844578, new AccessRightsT?(), "TestValue441473700", new DisplayFormat?()) }));
            _testClass = new MenuSetT(_identificationMenu, _parameterMenu, _observationMenu, _diagnosisMenu);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new MenuSetT(_identificationMenu, _parameterMenu, _observationMenu, _diagnosisMenu);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_MenuSetT()
        {
            // Arrange
            var same = new MenuSetT(_identificationMenu, _parameterMenu, _observationMenu, _diagnosisMenu);
            var different = new MenuSetT(new UIMenuRefSimpleT("TestValue772714388", new MenuT("TestValue716694849", "TestValue1409752393", new[] { new UIVariableRefT("TestValue815804439", 438448423.05M, 862865306.82M, (uint)12679151, new AccessRightsT?(), "TestValue703479755", new DisplayFormat?()), new UIVariableRefT("TestValue945221999", 1592104482.54M, 1000649025.09M, (uint)1648175622, new AccessRightsT?(), "TestValue1211671153", new DisplayFormat?()), new UIVariableRefT("TestValue691818472", 1817822552.04M, 183239294.04M, (uint)1431693904, new AccessRightsT?(), "TestValue1408870619", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue376219086", new ConditionT("TestValue1954036566", (byte)30, 230685622)), new UIMenuRefT("TestValue1488515208", new ConditionT("TestValue163154874", (byte)118, 759049631)), new UIMenuRefT("TestValue405065321", new ConditionT("TestValue1408288109", (byte)164, 513816197)) }, new[] { new UIRecordItemRefT("TestValue1137552364", (byte)133, 483588719.46M, 1688518035.27M, (uint)674694718, new AccessRightsT?(), "TestValue1666153623", new DisplayFormat?()), new UIRecordItemRefT("TestValue1545403101", (byte)3, 620877452.58M, 321357881.79M, (uint)805188016, new AccessRightsT?(), "TestValue174129284", new DisplayFormat?()), new UIRecordItemRefT("TestValue265793141", (byte)186, 1184127516.5M, 1368197037.9M, (uint)614975890, new AccessRightsT?(), "TestValue1821093163", new DisplayFormat?()) })), new UIMenuRefSimpleT("TestValue657587378", new MenuT("TestValue907999369", "TestValue12384184", new[] { new UIVariableRefT("TestValue641700062", 1485924082.83M, 1985238742.41M, (uint)187544627, new AccessRightsT?(), "TestValue542028766", new DisplayFormat?()), new UIVariableRefT("TestValue1375663288", 1234131543.81M, 1948240951.47M, (uint)1831311211, new AccessRightsT?(), "TestValue466515797", new DisplayFormat?()), new UIVariableRefT("TestValue751874689", 1447685672.4M, 427091743.98M, (uint)470633982, new AccessRightsT?(), "TestValue2030713481", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue1159702168", new ConditionT("TestValue1653581027", (byte)2, 170030773)), new UIMenuRefT("TestValue787170742", new ConditionT("TestValue1400065815", (byte)92, 51615165)), new UIMenuRefT("TestValue1148848550", new ConditionT("TestValue79799789", (byte)250, 2066881729)) }, new[] { new UIRecordItemRefT("TestValue1114550121", (byte)209, 142633003.59M, 724141246.95M, (uint)750700731, new AccessRightsT?(), "TestValue480655273", new DisplayFormat?()), new UIRecordItemRefT("TestValue501432013", (byte)58, 1756336217.13M, 624358787.58M, (uint)1518029146, new AccessRightsT?(), "TestValue132665952", new DisplayFormat?()), new UIRecordItemRefT("TestValue290490335", (byte)35, 209079667.17M, 933888164.22M, (uint)2005571771, new AccessRightsT?(), "TestValue53313327", new DisplayFormat?()) })), new UIMenuRefSimpleT("TestValue859304171", new MenuT("TestValue1981870986", "TestValue1098483144", new[] { new UIVariableRefT("TestValue1312858566", 220091891.58M, 1097789724.9M, (uint)1803969409, new AccessRightsT?(), "TestValue487556605", new DisplayFormat?()), new UIVariableRefT("TestValue120828627", 752203995.84M, 1987387411.68M, (uint)1449318252, new AccessRightsT?(), "TestValue856443109", new DisplayFormat?()), new UIVariableRefT("TestValue107744838", 571364922.15M, 1441702553.94M, (uint)132047398, new AccessRightsT?(), "TestValue207085783", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue1687237470", new ConditionT("TestValue1882543639", (byte)0, 981653487)), new UIMenuRefT("TestValue2077457436", new ConditionT("TestValue2122265327", (byte)30, 529036726)), new UIMenuRefT("TestValue135551881", new ConditionT("TestValue2031941449", (byte)240, 488365160)) }, new[] { new UIRecordItemRefT("TestValue1188874232", (byte)235, 191591770.59M, 1084579870.77M, (uint)1367979265, new AccessRightsT?(), "TestValue130505142", new DisplayFormat?()), new UIRecordItemRefT("TestValue968398585", (byte)231, 1523134134.72M, 1170852089.22M, (uint)1074970043, new AccessRightsT?(), "TestValue263144126", new DisplayFormat?()), new UIRecordItemRefT("TestValue359826646", (byte)224, 629515177.83M, 321511147.65M, (uint)1921705980, new AccessRightsT?(), "TestValue1410284308", new DisplayFormat?()) })), new UIMenuRefSimpleT("TestValue1703013314", new MenuT("TestValue981708776", "TestValue166758184", new[] { new UIVariableRefT("TestValue4105800", 1389531879.45M, 121797850.68M, (uint)52139369, new AccessRightsT?(), "TestValue2025134331", new DisplayFormat?()), new UIVariableRefT("TestValue2051897197", 1112464449.36M, 1045659851.28M, (uint)1840761963, new AccessRightsT?(), "TestValue973326629", new DisplayFormat?()), new UIVariableRefT("TestValue1919511607", 601936005.33M, 1762147642.86M, (uint)1718411431, new AccessRightsT?(), "TestValue21578086", new DisplayFormat?()) }, new[] { new UIMenuRefT("TestValue1255791211", new ConditionT("TestValue1908391533", (byte)105, 446631143)), new UIMenuRefT("TestValue487866621", new ConditionT("TestValue330355843", (byte)96, 504558592)), new UIMenuRefT("TestValue807573596", new ConditionT("TestValue1885513479", (byte)73, 188343792)) }, new[] { new UIRecordItemRefT("TestValue1486391410", (byte)246, 747266249.07M, 864023707.8M, (uint)328928135, new AccessRightsT?(), "TestValue1045315309", new DisplayFormat?()), new UIRecordItemRefT("TestValue321606975", (byte)140, 576493227.09M, 69793919.91M, (uint)1043395854, new AccessRightsT?(), "TestValue1490328581", new DisplayFormat?()), new UIRecordItemRefT("TestValue226091592", (byte)236, 884023598.7M, 1826788601.88M, (uint)209352249, new AccessRightsT?(), "TestValue1302942083", new DisplayFormat?()) })));

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
        public void IdentificationMenuIsInitializedCorrectly()
        {
            _testClass.IdentificationMenu.Should().BeSameAs(_identificationMenu);
        }

        [Fact]
        public void ParameterMenuIsInitializedCorrectly()
        {
            _testClass.ParameterMenu.Should().BeSameAs(_parameterMenu);
        }

        [Fact]
        public void ObservationMenuIsInitializedCorrectly()
        {
            _testClass.ObservationMenu.Should().BeSameAs(_observationMenu);
        }

        [Fact]
        public void DiagnosisMenuIsInitializedCorrectly()
        {
            _testClass.DiagnosisMenu.Should().BeSameAs(_diagnosisMenu);
        }
    }
}