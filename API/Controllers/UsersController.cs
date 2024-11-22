using Application.Commands.Users;
using Application.Queries.Users.GetAll;
using Application.Queries.Users.LogIn;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userToAdd)
        {
            return Ok(await _mediator.Send(new AddUserCommand(userToAdd)));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LogIn([FromBody] User userToLogIn)
        {
            return Ok(await _mediator.Send(new LogInUserQuery(userToLogIn)));
        }
    }
}
