using IdentityService.Application.Location.DTOs;
using MediatR;

namespace IdentityService.Application.Location.Queries.ResolveCity;

public sealed record ResolveCityQuery(string Uf, string City) : IRequest<CityResponse?>;
