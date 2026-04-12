namespace IdentityService.Application.Location.DTOs;

public sealed record CityResponse(
    int IbgeCode,
    string Name,
    string Uf,
    string Region
);
