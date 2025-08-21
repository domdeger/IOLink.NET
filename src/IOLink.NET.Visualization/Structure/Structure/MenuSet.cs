using IOLink.NET.Core.Contracts;
using IOLink.NET.Visualization.Structure.Interfaces;

namespace IOLink.NET.Visualization.Structure.Structure;

public record MenuSet(
    UIMenu IdentificationMenu,
    UIMenu? ParameterMenu,
    UIMenu? ObservationMenu,
    UIMenu? DiagnosisMenu,
    IIODDPortReader IoddPortReader
) : IReadable
{
    public async Task ReadAsync(CancellationToken cancellationToken)
    {
        await IdentificationMenu.ReadAsync(cancellationToken).ConfigureAwait(false);

        if (ParameterMenu is not null)
        {
            await ParameterMenu.ReadAsync(cancellationToken).ConfigureAwait(false);
        }

        if (ObservationMenu is not null)
        {
            await ObservationMenu.ReadAsync(cancellationToken).ConfigureAwait(false);
        }

        if (DiagnosisMenu is not null)
        {
            await DiagnosisMenu.ReadAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
