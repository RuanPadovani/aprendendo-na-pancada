using IdentityService.Application.Location.DTOs;

namespace IdentityService.Application.Common.Interfaces;

public interface ILocationProvider
{
    Task<CityResponse?> ResolveCityAsync(string uf, string city, CancellationToken ct);
}
