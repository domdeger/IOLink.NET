using IOLink.NET.IODD.Structure.Structure.Menu;

namespace IOLink.NET.IODD.Structure.Interfaces.Menu;
public interface IMenuSetT
{
    UIMenuRefSimpleT IdentificationMenu { get; }
    UIMenuRefSimpleT? ParameterMenu { get; } 
    UIMenuRefSimpleT? ObservationMenu { get; } 
    UIMenuRefSimpleT? DiagnosisMenu { get; }
}
