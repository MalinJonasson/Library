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
    public class AuthorController : Controller
    {
        internal readonly IMediator _mediator;
        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("addNewAuthor")]
        public async Task<IActionResult> AddNewAuthor([FromBody] Author authorToAdd)
        {
            try
            {
                var authorToBeAdded = await _mediator.Send(new AddAuthorCommand(authorToAdd));
                return Ok(authorToBeAdded);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var allAuthors = await _mediator.Send(new GetAllAuthorsQuery());
                return Ok(allAuthors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{authorId}")]
        public async Task<IActionResult> GetAuthorsById(Guid authorId)
        {
            try
            {
               var getAuthorById = await _mediator.Send(new GetAuthorsByIdQuery(authorId));
                return Ok(getAuthorById);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updateAuthor/{updatedAuthorId}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author updatedAuthor, Guid updatedAuthorId)
        {
            try
            {
                var authorToUpdate = await _mediator.Send(new UpdateAuthorByIdCommand(updatedAuthor, updatedAuthorId));
                return Ok(authorToUpdate);
            }
            catch (ArgumentNullException ex)
            {
               return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
                      
            
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteAuthor/{authorToDeleteId}")]
        public async Task<IActionResult> DeleteAuthor([FromBody] Guid authorToDeleteId)
        {
            try
            {
                var authorToDelete = await _mediator.Send(new DeleteAuthorByIdCommand(authorToDeleteId));
                return Ok(authorToDelete);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);

            }

        }
    }
}
