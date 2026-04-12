using IdentityService.Api.Contracts.Users;
using IdentityService.Application.Users.Commands.CreateUser;
using IdentityService.Application.Users.Commands.DeleteUser;
using IdentityService.Application.Users.Commands.UpdateUser;
using IdentityService.Application.Users.Queries.GetAllUsers;
using IdentityService.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet(Name = "ListAllUsers")]
    [Authorize]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _sender.Send(new GetAllUsersQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}", Name = "GetUserById")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken ct)
    {
        var result = await _sender.Send(new GetUserByIdQuery(id), ct);

        if (result is null)
            return NotFound("Usuário não encontrado.");

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest request, CancellationToken ct)
    {
        var command = new CreateUserCommand(request.Name, request.Email, request.Password);
        var result = await _sender.Send(command, ct);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetUserById), new { id = result.Value }, null);
    }

    [HttpPut("{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> Put(Guid userId, [FromBody] UpdateUserRequest request, CancellationToken ct)
    {
        var command = new UpdateUserCommand(userId, request.Name, request.Email);
        var result = await _sender.Send(command, ct);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpDelete("{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid userId, CancellationToken ct)
    {
        var command = new DeleteUserCommand(userId);
        var result = await _sender.Send(command, ct);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }
}
