namespace IdentityService.Application.Results.Locations;

public sealed record CityResult(
    int IbgeCode,
    string Name,
    string Uf,
    string Region
);
