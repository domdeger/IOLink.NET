using IOLink.NET.Core.Contracts;
using IOLink.NET.Visualization.Structure.Interfaces;

namespace IOLink.NET.Visualization.Structure.Structure
{
    public record RoleMenu(
        UIMenu IdentificationMenu,
        UIMenu? ParameterMenu,
        UIMenu? ObservationMenu,
        UIMenu? DiagnosisMenu,
        IIODDPortReader IoddPortReader
    ) : IReadable
    {
        public async Task ReadAsync()
        {
            await IdentificationMenu.ReadAsync();

            if (ParameterMenu is not null)
            {
                await ParameterMenu.ReadAsync();
            }

            if (ObservationMenu is not null)
            {
                await ObservationMenu.ReadAsync();
            }

            if (DiagnosisMenu is not null)
            {
                await DiagnosisMenu.ReadAsync();
            }
        }
    }
}
