namespace IODD.Structure.Tests.Profile
{
    using System.Collections.Generic;

    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Common;
    using IOLinkNET.IODD.Structure.Datatypes;
    using IOLinkNET.IODD.Structure.DeviceFunction;
    using IOLinkNET.IODD.Structure.Interfaces.Menu;
    using IOLinkNET.IODD.Structure.ProcessData;
    using IOLinkNET.IODD.Structure.Profile;

    using NSubstitute;

    using Xunit;

    public class DeviceFunctionTTests
    {
        private readonly DeviceFunctionT _testClass;
        private readonly IEnumerable<DatatypeT> _datatypeCollection;
        private readonly IEnumerable<VariableT> _variableCollection;
        private readonly IEnumerable<ProcessDataT> _processDataCollection;
        private readonly IUserInterfaceT _userInterface;

        public DeviceFunctionTTests()
        {
            _datatypeCollection = new[] { new UIntegerT("TestValue1522824151", (ushort)30408, default!, default!), new UIntegerT("TestValue1724441888", (ushort)50198, default!, default!), new UIntegerT("TestValue1229716329", (ushort)1733, default!, default!) };
            _variableCollection = new[] { new VariableT("TestValue1687780692", (ushort)18126, new UIntegerT("TestValue482469254", (ushort)5371, default!, default!), new DatatypeRefT("TestValue131498409"), new TextRefT("TestValue255895736"), new TextRefT("TestValue1598913686"), AccessRightsT.ReadWrite, default!, true, true, true), new VariableT("TestValue152814478", (ushort)55432, new UIntegerT("TestValue491620508", (ushort)63450, default!, default!), new DatatypeRefT("TestValue598905026"), new TextRefT("TestValue1244430883"), new TextRefT("TestValue145597972"), AccessRightsT.ReadWrite, default!, true, false, true), new VariableT("TestValue535838095", (ushort)53086, new UIntegerT("TestValue1001681665", (ushort)61023, default!, default!), new DatatypeRefT("TestValue1734742778"), new TextRefT("TestValue488348421"), new TextRefT("TestValue1944273023"), AccessRightsT.ReadOnly, default!, true, false, true) };
            _processDataCollection = new[] { new ProcessDataT(new ConditionT("TestValue1652115074", (byte)180, 1976346042), new ProcessDataItemT(new UIntegerT("TestValue1666185608", (ushort)36067, default!, default!), new DatatypeRefT("TestValue318938493"), "TestValue117709510", (ushort)5441), new ProcessDataItemT(new UIntegerT("TestValue1836132099", (ushort)12422, default!, default!), new DatatypeRefT("TestValue1589633163"), "TestValue310837098", (ushort)9878)), new ProcessDataT(new ConditionT("TestValue831853412", (byte)154, 719538467), new ProcessDataItemT(new UIntegerT("TestValue923919125", (ushort)57966, default!, default!), new DatatypeRefT("TestValue1578843916"), "TestValue599463915", (ushort)37028), new ProcessDataItemT(new UIntegerT("TestValue1643599806", (ushort)42991, default!, default!), new DatatypeRefT("TestValue58132935"), "TestValue890400127", (ushort)40337)), new ProcessDataT(new ConditionT("TestValue279333599", (byte)79, 1110565265), new ProcessDataItemT(new UIntegerT("TestValue1451616088", (ushort)54398, default!, default!), new DatatypeRefT("TestValue894875648"), "TestValue1522040802", (ushort)13061), new ProcessDataItemT(new UIntegerT("TestValue164311387", (ushort)34056, default!, default!), new DatatypeRefT("TestValue102829884"), "TestValue1080203065", (ushort)1908)) };
            _userInterface = Substitute.For<IUserInterfaceT>();
            _testClass = new DeviceFunctionT(_datatypeCollection, _variableCollection, _processDataCollection, _userInterface);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new DeviceFunctionT(_datatypeCollection, _variableCollection, _processDataCollection, _userInterface);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_DeviceFunctionT()
        {
            // Arrange
            var same = new DeviceFunctionT(_datatypeCollection, _variableCollection, _processDataCollection, _userInterface);
            var different = new DeviceFunctionT(new[] { new UIntegerT("TestValue1974143126", (ushort)28311, default!, default!), new UIntegerT("TestValue1985503736", (ushort)26611, default!, default!), new UIntegerT("TestValue2019431535", (ushort)33376, default!, default!) }, new[] { new VariableT("TestValue1428715676", (ushort)20690, new UIntegerT("TestValue1167795045", (ushort)13305, default!, default!), new DatatypeRefT("TestValue1916293601"), new TextRefT("TestValue1020353898"), new TextRefT("TestValue381229749"), AccessRightsT.WriteOnly, default!, true, true, false), new VariableT("TestValue114349795", (ushort)58538, new UIntegerT("TestValue775785481", (ushort)61264, default!, default!), new DatatypeRefT("TestValue39604894"), new TextRefT("TestValue1596556552"), new TextRefT("TestValue1215233298"), AccessRightsT.ReadOnly, default!, true, true, true), new VariableT("TestValue2043171986", (ushort)20371, new UIntegerT("TestValue182758835", (ushort)18820, default!, default!), new DatatypeRefT("TestValue1991199707"), new TextRefT("TestValue1836949282"), new TextRefT("TestValue1752184437"), AccessRightsT.WriteOnly, default!, true, false, true) }, new[] { new ProcessDataT(new ConditionT("TestValue1111928004", (byte)191, 643830627), new ProcessDataItemT(new UIntegerT("TestValue1259022201", (ushort)7099, default!, default!), new DatatypeRefT("TestValue1341755311"), "TestValue431262190", (ushort)42764), new ProcessDataItemT(new UIntegerT("TestValue1598042173", (ushort)35252, default!, default!), new DatatypeRefT("TestValue1916584122"), "TestValue155796119", (ushort)30567)), new ProcessDataT(new ConditionT("TestValue332100425", (byte)36, 202458684), new ProcessDataItemT(new UIntegerT("TestValue1934538232", (ushort)45124, default!, default!), new DatatypeRefT("TestValue770270438"), "TestValue487515005", (ushort)38068), new ProcessDataItemT(new UIntegerT("TestValue398931064", (ushort)15027, default!, default!), new DatatypeRefT("TestValue782296349"), "TestValue500253020", (ushort)7728)), new ProcessDataT(new ConditionT("TestValue1299585018", (byte)139, 691764114), new ProcessDataItemT(new UIntegerT("TestValue1839616332", (ushort)21213, default!, default!), new DatatypeRefT("TestValue1431030575"), "TestValue1384430125", (ushort)22125), new ProcessDataItemT(new UIntegerT("TestValue806285102", (ushort)40102, default!, default!), new DatatypeRefT("TestValue1363731462"), "TestValue928066340", (ushort)41622)) }, Substitute.For<IUserInterfaceT>());

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
        public void DatatypeCollectionIsInitializedCorrectly()
        {
            _testClass.DatatypeCollection.Should().BeSameAs(_datatypeCollection);
        }

        [Fact]
        public void VariableCollectionIsInitializedCorrectly()
        {
            _testClass.VariableCollection.Should().BeSameAs(_variableCollection);
        }

        [Fact]
        public void ProcessDataCollectionIsInitializedCorrectly()
        {
            _testClass.ProcessDataCollection.Should().BeSameAs(_processDataCollection);
        }

        [Fact]
        public void UserInterfaceIsInitializedCorrectly()
        {
            _testClass.UserInterface.Should().BeSameAs(_userInterface);
        }
    }
}