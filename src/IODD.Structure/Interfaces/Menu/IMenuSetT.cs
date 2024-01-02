using IOLinkNET.IODD.Structure.Structure.Menu;

namespace IOLinkNET.IODD.Structure.Interfaces.Menu;
public interface IMenuSetT
{
    UIMenuRefSimpleT IdentificationMenu { get; }
    UIMenuRefSimpleT? ParameterMenu { get; } 
    UIMenuRefSimpleT? ObservationMenu { get; } 
    UIMenuRefSimpleT? DiagnosisMenu { get; }
}
