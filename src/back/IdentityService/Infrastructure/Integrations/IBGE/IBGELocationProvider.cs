using IdentityService.Application.Common.Interfaces;
using IdentityService.Application.Location.DTOs;

namespace IdentityService.Infrastructure.Integrations.IBGE;

/// <summary>
/// Implementação pendente: integração com a API do IBGE.
/// </summary>
public sealed class IBGELocationProvider : ILocationProvider
{
    public Task<CityResponse?> ResolveCityAsync(string uf, string city, CancellationToken ct)
    {
        throw new NotImplementedException("Integração com o IBGE ainda não foi implementada.");
    }
}
