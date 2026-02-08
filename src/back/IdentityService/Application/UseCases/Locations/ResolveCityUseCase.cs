using IdentityService.Application.Interfaces;
using IdentityService.Application.Results.Locations;

namespace IdentityService.Application.UseCases.Locations;

public sealed class ResolveCityUseCase
{
    private readonly ILocationProvider _locationProvider;
    public ResolveCityUseCase(ILocationProvider locationProvider)
    {
        _locationProvider = locationProvider;
    }

    public async Task<CityResult> ExecuteAsync(string uf, string city, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(uf))
            throw new ArgumentException("UF é obrigatória!", nameof(uf));

        if (string.IsNullOrEmpty(city))
            throw new ArgumentException("Cidade é obrigatório.", nameof(city));

        uf = uf.Trim().ToUpperInvariant();
        city = city.Trim();

        var resolved = await _locationProvider.ResolveCityAsync(uf, city, ct);
        if (resolved is null)
            throw new InvalidOperationException($"Município não encontrado: '{city}' / '{uf}'.");

        return resolved;
    }
}
