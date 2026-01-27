namespace IdentityService.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
    {
        var result = await _userService.ListAllUsers();

        if (result == null)
            return NotFound("Users not found!");
            
        


        return Ok(result.Select(r => r.ToResponse()));
    }

    [HttpGet("{id:guid}", Name = "GetUserById")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
    {
        var results = await _userService.GetUserById(id);

        if (results is null)
            return NotFound("Users not found!");


        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateUserRequest request)
    {
        if (request == null)
            return BadRequest("Invalid Data");

        var userId = await _userService.CreateUser(request.ToCommand());

        return CreatedAtAction(nameof(GetUserById), new {id = userId}, null);
    }

    [HttpPut("{userId:guid}")]
    public async Task<ActionResult> Put(Guid userId, [FromBody]UpdateUserRequest request)
    {
        await _userService.EditUser(request.ToCommand(userId));
        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> Delete(Guid userId)
    {
        var existing = await _userService.GetUserById(userId);
        if (existing is null)
            return BadRequest();

        await _userService.DeleteUser(userId);

        return Ok("Usuario deletado");
    }
}
