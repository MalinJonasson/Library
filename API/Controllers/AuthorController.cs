using Application.Commands.Authors.AddAuthor;
using Application.Commands.Authors.DeleteAuthor;
using Application.Commands.Authors.UpdateAuthor;
using Application.Queries.Authors.GetAll;
using Application.Queries.Authors.GetById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IMediator mediator, ILogger<AuthorController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        [Route("addNewAuthor")]
        public async Task<IActionResult> AddNewAuthor([FromBody] Author authorToAdd)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for AddNewAuthor.");
                return BadRequest(ModelState);
            }

            try
            {
                var addNewAuthorOperationResult = await _mediator.Send(new AddAuthorCommand(authorToAdd));

                if (addNewAuthorOperationResult.IsSuccess)
                {
                    _logger.LogInformation("Successfully added a new author with ID: {AuthorId}", addNewAuthorOperationResult.Data.Id);
                    return Ok(new
                    {
                        message = addNewAuthorOperationResult.Message,
                        author = addNewAuthorOperationResult.Data
                    });
                }

                _logger.LogWarning("Failed to add author: {Error}", addNewAuthorOperationResult.Message);
                return BadRequest(new { message = addNewAuthorOperationResult.Message, errors = addNewAuthorOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in AddNewAuthor.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        [Route("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var getAllAuthorsOperationResult = await _mediator.Send(new GetAllAuthorsQuery());

                if (getAllAuthorsOperationResult.IsSuccess)
                {
                    return Ok(getAllAuthorsOperationResult.Data);
                }

                _logger.LogWarning("Failed to retrieve authors: {Error}", getAllAuthorsOperationResult.Message);
                return BadRequest(new { message = getAllAuthorsOperationResult.Message, errors = getAllAuthorsOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllAuthors.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        [Route("{authorId:guid}")]
        public async Task<IActionResult> GetAuthorById(Guid authorId)
        {
            try
            {
                var getAuthorByIdOperationResult = await _mediator.Send(new GetAuthorsByIdQuery(authorId));

                if (getAuthorByIdOperationResult.IsSuccess)
                {
                    return Ok(getAuthorByIdOperationResult.Data);
                }

                _logger.LogWarning("Author with ID: {AuthorId} not found.", authorId);
                return NotFound(new { message = getAuthorByIdOperationResult.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAuthorById.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updateAuthor/{updatedAuthorId:guid}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author updatedAuthor, Guid updatedAuthorId)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateAuthor.");
                return BadRequest(ModelState);
            }

            try
            {
                var updateAuthorByIdOperationResult = await _mediator.Send(new UpdateAuthorByIdCommand(updatedAuthor, updatedAuthorId));

                if (updateAuthorByIdOperationResult.IsSuccess)
                {
                    return Ok(new { message = updateAuthorByIdOperationResult.Message });
                }

                _logger.LogWarning("Failed to update author: {Error}", updateAuthorByIdOperationResult.Message);
                return BadRequest(new { message = updateAuthorByIdOperationResult.Message, errors = updateAuthorByIdOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in UpdateAuthor.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteAuthor/{authorToDeleteId:guid}")]
        public async Task<IActionResult> DeleteAuthor(Guid authorToDeleteId)
        {
            try
            {
                var deleteAuthorByIdOperationResult = await _mediator.Send(new DeleteAuthorByIdCommand(authorToDeleteId));

                if (deleteAuthorByIdOperationResult.IsSuccess)
                {
                    return Ok(new { message = deleteAuthorByIdOperationResult.Message });
                }

                _logger.LogWarning("Failed to delete author: {Error}", deleteAuthorByIdOperationResult.Message);
                return BadRequest(new { message = deleteAuthorByIdOperationResult.Message, errors = deleteAuthorByIdOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in DeleteAuthor.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }

}
