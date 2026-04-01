using IdentityService.API.Contracts.Users;
using IdentityService.API.Mappers;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name = "ListAllUsers")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll()
    {
        var result = await _userService.ListAllUsersAsync();

        if (result == null)
            return NotFound("Users not found!");
            

        return Ok(result.Select(r => r.ToResponse()));
    }
    
    [HttpGet("{id:guid}", Name = "GetUserById")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
    {
        var results = await _userService.GetUserByIdAsync(id);

        if (results is null)
            return NotFound("Users not found!");

        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateUserRequest request)
    {
        if (request == null)
            return BadRequest("Invalid Data");

        var userId = await _userService.CreateUserAsync(request.ToCommand());

        return CreatedAtAction(nameof(GetUserById), new {id = userId},  null);
    }

    [HttpPut("{userId:guid}")]
    public async Task<ActionResult> Put(Guid userId, [FromBody]UpdateUserRequest request)
    {
        await _userService.EditUserAsync(request.ToCommand(userId));
        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> Delete(Guid userId)
    {
        var existing = await _userService.GetUserByIdAsync(userId);
        if (existing is null)
            return BadRequest();

        await _userService.DeleteUserAsync(userId);

        return Ok("Usuario deletado");
    }
    
}
