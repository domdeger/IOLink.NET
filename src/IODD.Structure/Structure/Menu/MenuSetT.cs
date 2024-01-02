using IOLinkNET.IODD.Structure.Interfaces.Menu;

namespace IOLinkNET.IODD.Structure.Structure.Menu;
public record MenuSetT(UIMenuRefSimpleT IdentificationMenu, UIMenuRefSimpleT? ParameterMenu, UIMenuRefSimpleT? ObservationMenu, UIMenuRefSimpleT? DiagnosisMenu): IMenuSetT;
