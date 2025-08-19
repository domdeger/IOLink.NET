using IOLink.NET.Conversion;
using IOLink.NET.Core.Contracts;
using IOLink.NET.Integration;
using IOLink.NET.IODD.Provider;
using IOLink.NET.IODD.Resolution.Contracts;
using IOLink.NET.IODD.Structure.Interfaces;
using IOLink.NET.IODD.Structure.Structure.Menu;
using IOLink.NET.Visualization.IODDConversion;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;

namespace IOLink.NET.Tests;

public class IODDUserInterfaceConverterTests
{
    [Fact]
    public void ConversionThrowsIfUserInterfaceIsNotPresent()
    {
        var deviceSub = Substitute.For<IIODevice>();
        deviceSub.ProfileBody.DeviceFunction.UserInterface.ReturnsNull();
        var ioddUserInterfaceConverter = new IODDUserInterfaceConverter(
            deviceSub,
            GetSubstituteForIODDPortReader()
        );

        var convertAction = () => ioddUserInterfaceConverter.Convert();

        Should.Throw<ArgumentNullException>(convertAction, "User Interface not present in IODD");
    }

    [Fact]
    public void MissingObserverRoleMenuIdentificationSubMenuShouldThrow()
    {
        var deviceSub = Substitute.For<IIODevice>();
        deviceSub.ProfileBody.DeviceFunction.UserInterface.ObserverRoleMenuSet.IdentificationMenu.Returns(
            new UIMenuRefSimpleT(null, null)
        );
        var ioddUserInterfaceConverter = new IODDUserInterfaceConverter(
            deviceSub,
            GetSubstituteForIODDPortReader()
        );
        var convertAction = () => ioddUserInterfaceConverter.Convert();

        Should.Throw<InvalidOperationException>(
            convertAction,
            "Observerrole Menu must provide Identification Menu"
        );
    }

    [Fact]
    public void MissingMaintenanceRoleMenuIdentificationSubMenuShouldThrow()
    {
        var deviceSub = Substitute.For<IIODevice>();
        var menuList = new List<MenuCollectionT>()
        {
            new(new("M_OR_Ident", null, null, null, null)),
        };
        deviceSub.ProfileBody.DeviceFunction.UserInterface.MenuCollection.Returns(menuList);
        deviceSub.ProfileBody.DeviceFunction.UserInterface.ObserverRoleMenuSet.IdentificationMenu.Returns(
            new UIMenuRefSimpleT("M_OR_Ident", null)
        );
        deviceSub.ProfileBody.DeviceFunction.UserInterface.MaintenanceRoleMenuSet.IdentificationMenu.Returns(
            new UIMenuRefSimpleT(null, null)
        );

        var ioddUserInterfaceConverter = new IODDUserInterfaceConverter(
            deviceSub,
            GetSubstituteForIODDPortReader()
        );
        var convertAction = () => ioddUserInterfaceConverter.Convert();

        Should.Throw<InvalidOperationException>(
            convertAction,
            "Maintenancerole Menu must provide Identification Menu"
        );
    }

    [Fact]
    public void MissingSpecialistRoleMenuIdentificationSubMenuShouldThrow()
    {
        var deviceSub = Substitute.For<IIODevice>();

        var menuList = new List<MenuCollectionT>()
        {
            new(new("M_OR_Ident", null, null, null, null)),
            new(new("M_MR_SR_Ident", null, null, null, null)),
        };

        deviceSub.ProfileBody.DeviceFunction.UserInterface.MenuCollection.Returns(menuList);
        deviceSub.ProfileBody.DeviceFunction.UserInterface.ObserverRoleMenuSet.IdentificationMenu.Returns(
            new UIMenuRefSimpleT("M_OR_Ident", null)
        );
        deviceSub.ProfileBody.DeviceFunction.UserInterface.MaintenanceRoleMenuSet.IdentificationMenu.Returns(
            new UIMenuRefSimpleT("M_MR_SR_Ident", null)
        );
        deviceSub.ProfileBody.DeviceFunction.UserInterface.SpecialistRoleMenuSet.IdentificationMenu.Returns(
            new UIMenuRefSimpleT(null, null)
        );
        var ioddUserInterfaceConverter = new IODDUserInterfaceConverter(
            deviceSub,
            GetSubstituteForIODDPortReader()
        );
        var convertAction = () => ioddUserInterfaceConverter.Convert();

        Should.Throw<InvalidOperationException>(
            convertAction,
            "Specialistrole Menu must provide Identification Menu"
        );
    }

    private static IIODDPortReader GetSubstituteForIODDPortReader()
    {
        return Substitute.For<IIODDPortReader>();
    }
}
