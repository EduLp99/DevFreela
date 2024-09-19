using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            var messages = ModelState
                .SelectMany(ms => ms.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(messages);
        }

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id }, command);
    }

    // api/users/login
    [HttpPut("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var loginUserViewModel = await _mediator.Send(command);

        if (loginUserViewModel == null)
        {
            return BadRequest();
        }

        return Ok(loginUserViewModel);
    }
}