using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[Route("[controller]")]
public class LocationsController : Controller
{
    private readonly ILogger<LocationsController> _logger;

    public LocationsController(ILogger<LocationsController> logger)
    {
        _logger = logger;
    }


    [HttpGet(Name ="para")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}
