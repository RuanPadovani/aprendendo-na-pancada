using IdentityService.Application.Common.Interfaces;
using IdentityService.Application.Location.DTOs;
using MediatR;

namespace IdentityService.Application.Location.Queries.ResolveCity;

public sealed class ResolveCityQueryHandler : IRequestHandler<ResolveCityQuery, CityResponse?>
{
    private readonly ILocationProvider _locationProvider;

    public ResolveCityQueryHandler(ILocationProvider locationProvider)
    {
        _locationProvider = locationProvider;
    }

    public async Task<CityResponse?> Handle(ResolveCityQuery request, CancellationToken cancellationToken)
    {
        return await _locationProvider.ResolveCityAsync(request.Uf, request.City, cancellationToken);
    }
}
