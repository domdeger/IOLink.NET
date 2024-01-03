using IOLinkNET.Integration;
using IOLinkNET.Visualization.Structure.Interfaces;

namespace IOLinkNET.Visualization.Structure.Structure;
public record MenuSet(UIMenu IdentificationMenu, UIMenu? ParameterMenu, UIMenu? ObservationMenu, UIMenu? DiagnosisMenu, IODDPortReader IoddPortReader) : IReadable
{
    public async Task ReadAsync()
    {
        await IdentificationMenu.ReadAsync();

        if (ParameterMenu != null)
        {
            await ParameterMenu.ReadAsync();
        }

        if (ObservationMenu != null)
        {
            await ObservationMenu.ReadAsync();
        }

        if (DiagnosisMenu != null)
        {
            await DiagnosisMenu.ReadAsync();
        }
    }
}
