namespace IODD.Structure.Tests.Structure.Menu
{
    using System.Collections.Generic;

    using FluentAssertions;

    using IOLinkNET.IODD.Structure.Interfaces.Menu;
    using IOLinkNET.IODD.Structure.Structure.Menu;

    using NSubstitute;

    using Xunit;

    public class UserInterfaceTTests
    {
        private readonly UserInterfaceT _testClass;
        private readonly IEnumerable<MenuCollectionT> _menuCollection;
        private readonly IMenuSetT _observerRoleMenuSet;
        private readonly IMenuSetT _maintenanceRoleMenuSet;
        private readonly IMenuSetT _specialistRoleMenuSet;

        public UserInterfaceTTests()
        {
            _menuCollection = new[] { new MenuCollectionT(new MenuT("TestValue1654658386", "TestValue414845818", default(IEnumerable<UIVariableRefT>), default(IEnumerable<UIMenuRefT>), default(IEnumerable<UIRecordItemRefT>))), new MenuCollectionT(new MenuT("TestValue357917563", "TestValue1489288397", default(IEnumerable<UIVariableRefT>), default(IEnumerable<UIMenuRefT>), default(IEnumerable<UIRecordItemRefT>))), new MenuCollectionT(new MenuT("TestValue1081199397", "TestValue443371185", default(IEnumerable<UIVariableRefT>), default(IEnumerable<UIMenuRefT>), default(IEnumerable<UIRecordItemRefT>))) };
            _observerRoleMenuSet = Substitute.For<IMenuSetT>();
            _maintenanceRoleMenuSet = Substitute.For<IMenuSetT>();
            _specialistRoleMenuSet = Substitute.For<IMenuSetT>();
            _testClass = new UserInterfaceT(_menuCollection, _observerRoleMenuSet, _maintenanceRoleMenuSet, _specialistRoleMenuSet);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var instance = new UserInterfaceT(_menuCollection, _observerRoleMenuSet, _maintenanceRoleMenuSet, _specialistRoleMenuSet);

            // Assert
            instance.Should().NotBeNull();
        }

        [Fact]
        public void ImplementsIEquatable_UserInterfaceT()
        {
            // Arrange
            var same = new UserInterfaceT(_menuCollection, _observerRoleMenuSet, _maintenanceRoleMenuSet, _specialistRoleMenuSet);
            var different = new UserInterfaceT(new[] { new MenuCollectionT(new MenuT("TestValue1100128949", "TestValue1419079869", default(IEnumerable<UIVariableRefT>), default(IEnumerable<UIMenuRefT>), default(IEnumerable<UIRecordItemRefT>))), new MenuCollectionT(new MenuT("TestValue1748394587", "TestValue1160093053", default(IEnumerable<UIVariableRefT>), default(IEnumerable<UIMenuRefT>), default(IEnumerable<UIRecordItemRefT>))), new MenuCollectionT(new MenuT("TestValue1816214901", "TestValue1325324903", default(IEnumerable<UIVariableRefT>), default(IEnumerable<UIMenuRefT>), default(IEnumerable<UIRecordItemRefT>))) }, Substitute.For<IMenuSetT>(), Substitute.For<IMenuSetT>(), Substitute.For<IMenuSetT>());

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
        public void MenuCollectionIsInitializedCorrectly()
        {
            _testClass.MenuCollection.Should().BeSameAs(_menuCollection);
        }

        [Fact]
        public void ObserverRoleMenuSetIsInitializedCorrectly()
        {
            _testClass.ObserverRoleMenuSet.Should().BeSameAs(_observerRoleMenuSet);
        }

        [Fact]
        public void MaintenanceRoleMenuSetIsInitializedCorrectly()
        {
            _testClass.MaintenanceRoleMenuSet.Should().BeSameAs(_maintenanceRoleMenuSet);
        }

        [Fact]
        public void SpecialistRoleMenuSetIsInitializedCorrectly()
        {
            _testClass.SpecialistRoleMenuSet.Should().BeSameAs(_specialistRoleMenuSet);
        }
    }
}