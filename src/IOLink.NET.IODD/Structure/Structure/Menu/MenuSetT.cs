using IOLink.NET.IODD.Structure.Interfaces.Menu;

namespace IOLink.NET.IODD.Structure.Structure.Menu;
public record MenuSetT(UIMenuRefSimpleT IdentificationMenu, UIMenuRefSimpleT? ParameterMenu, UIMenuRefSimpleT? ObservationMenu, UIMenuRefSimpleT? DiagnosisMenu): IMenuSetT;
