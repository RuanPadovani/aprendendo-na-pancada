using Application.Common.Mediator;
using IdentityService.Application.Location.DTOs;

namespace IdentityService.Application.Location.Queries.ResolveCity;

public sealed record ResolveCityQuery(string Uf, string City) : IRequest<CityResponse?>;
