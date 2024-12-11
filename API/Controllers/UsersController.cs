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

        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var getAllUsersOperationResult = await _mediator.Send(new GetAllUsersQuery());

                if (getAllUsersOperationResult.IsSuccess)
                {
                    _logger.LogInformation("Successfully retrieved all users.");
                    return Ok(getAllUsersOperationResult.Data);
                }

                _logger.LogWarning("Failed to retrieve users: {Error}", getAllUsersOperationResult.Message);
                return BadRequest(new { message = getAllUsersOperationResult.Message, errors = getAllUsersOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllUsers.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userToAdd)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for RegisterUser.");
                return BadRequest(ModelState);
            }

            try
            {
                var registerUserOperationResult = await _mediator.Send(new AddUserCommand(userToAdd));

                if (registerUserOperationResult.IsSuccess)
                {
                    _logger.LogInformation("Successfully registered user with ID: {UserId}", registerUserOperationResult.Data.Id);
                    return Ok(new { message = registerUserOperationResult.Message, user = registerUserOperationResult.Data });
                }

                _logger.LogWarning("Failed to register user: {Error}", registerUserOperationResult.Message);
                return BadRequest(new { message = registerUserOperationResult.Message, errors = registerUserOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in RegisterUser.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LogIn([FromBody] User userToLogIn)
        {
            try
            {
                var userToBeLoggedIn = await _mediator.Send(new LogInUserQuery(userToLogIn));
                return Ok(userToBeLoggedIn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
