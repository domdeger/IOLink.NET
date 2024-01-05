using FluentAssertions;

using IOLinkNET.Conversion;
using IOLinkNET.Device.Contract;
using IOLinkNET.Integration;
using IOLinkNET.IODD.Provider;
using IOLinkNET.IODD.Resolution.Contracts;
using IOLinkNET.IODD.Structure.Interfaces;
using IOLinkNET.IODD.Structure.Structure.Menu;
using IOLinkNET.Visualization.IODDConversion;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Visualization.Tests;
public class IODDUserInterfaceConverterTests
{
    [Fact]
    public void ConversionThrowsIfUserInterfaceIsNotPresent()
    {
        var deviceSub = Substitute.For<IIODevice>();
        deviceSub.ProfileBody.DeviceFunction.UserInterface.ReturnsNull();
        var ioddUserInterfaceConverter = new IODDUserInterfaceConverter(deviceSub, GetSubstituteForIODDPortReader());
        
        var convertAction = () => ioddUserInterfaceConverter.Convert();

        convertAction.Should().Throw<ArgumentNullException>().WithMessage("*User Interface not present in IODD*");
    }

    [Fact]
    public void MissingObserverRoleMenuIdentificationSubMenuShouldThrow()
    {
        var deviceSub = Substitute.For<IIODevice>();
        deviceSub.ProfileBody.DeviceFunction.UserInterface.ObserverRoleMenuSet.IdentificationMenu.Returns(new UIMenuRefSimpleT(null, null));
        var ioddUserInterfaceConverter = new IODDUserInterfaceConverter(deviceSub, GetSubstituteForIODDPortReader());
        var convertAction = () => ioddUserInterfaceConverter.Convert();

        convertAction.Should().Throw<InvalidOperationException>().WithMessage("*Observerrole Menu must provide Identification Menu*");
    }

    [Fact]
    public void MissingMaintenanceRoleMenuIdentificationSubMenuShouldThrow()
    {
        var deviceSub = Substitute.For<IIODevice>();
        var menuList = new List<MenuCollectionT>() { 
            new(new("M_OR_Ident", null, null, null, null))
        };
        deviceSub.ProfileBody.DeviceFunction.UserInterface.MenuCollection.Returns(menuList);
        deviceSub.ProfileBody.DeviceFunction.UserInterface.ObserverRoleMenuSet.IdentificationMenu.Returns(new UIMenuRefSimpleT("M_OR_Ident", null));
        deviceSub.ProfileBody.DeviceFunction.UserInterface.MaintenanceRoleMenuSet.IdentificationMenu.Returns(new UIMenuRefSimpleT(null, null));

        var ioddUserInterfaceConverter = new IODDUserInterfaceConverter(deviceSub, GetSubstituteForIODDPortReader());
        var convertAction = () => ioddUserInterfaceConverter.Convert();

        convertAction.Should().Throw<InvalidOperationException>().WithMessage("*Maintenancerole Menu must provide Identification Menu*");
    }

    [Fact]
    public void MissingSpecialistRoleMenuIdentificationSubMenuShouldThrow()
    {
        var deviceSub = Substitute.For<IIODevice>();

        var menuList = new List<MenuCollectionT>() {
            new(new("M_OR_Ident", null, null, null, null)),
            new(new("M_MR_SR_Ident", null, null, null, null))
        };

        deviceSub.ProfileBody.DeviceFunction.UserInterface.MenuCollection.Returns(menuList);
        deviceSub.ProfileBody.DeviceFunction.UserInterface.ObserverRoleMenuSet.IdentificationMenu.Returns(new UIMenuRefSimpleT("M_OR_Ident", null));
        deviceSub.ProfileBody.DeviceFunction.UserInterface.MaintenanceRoleMenuSet.IdentificationMenu.Returns(new UIMenuRefSimpleT("M_MR_SR_Ident", null));
        deviceSub.ProfileBody.DeviceFunction.UserInterface.SpecialistRoleMenuSet.IdentificationMenu.Returns(new UIMenuRefSimpleT(null, null));
        var ioddUserInterfaceConverter = new IODDUserInterfaceConverter(deviceSub, GetSubstituteForIODDPortReader());
        var convertAction = () => ioddUserInterfaceConverter.Convert();

        convertAction.Should().Throw<InvalidOperationException>().WithMessage("*Specialistrole Menu must provide Identification Menu*");
    }

    private static IODDPortReader GetSubstituteForIODDPortReader()
    {
        return Substitute.For<IODDPortReader>(Substitute.For<IMasterConnection>(), Substitute.For<IDeviceDefinitionProvider>(), Substitute.For<IIoddDataConverter>(), Substitute.For<ITypeResolverFactory>());
    }

}
