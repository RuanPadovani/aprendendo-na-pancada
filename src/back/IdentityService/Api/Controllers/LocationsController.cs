using IdentityService.Application.Location.Queries.ResolveCity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ISender _sender;

    public LocationsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{uf}/{city}")]
    public async Task<IActionResult> GetCity(string uf, string city, CancellationToken ct)
    {
        var result = await _sender.Send(new ResolveCityQuery(uf, city), ct);

        if (result is null)
            return NotFound("Município não encontrado.");

        return Ok(result);
    }
}
