using Application.Commands.AddAuthor;
using Application.Commands.DeleteAuthor;
using Application.Commands.UpdateAuthor;
using Application.Queries.GetAll;
using Application.Queries.GetAllById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        //CRUD GET UPDATE/PUT/PATCH POST DELETE

        internal readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("addNewAuthor")]
        public async void AddNewAuthor([FromBody] Author authorToAdd)
        {
            await _mediator.Send(new AddAuthorCommand(authorToAdd));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _mediator.Send(new GetAllAuthorsQuery()));
        }

        [HttpGet]
        [Route("{authorId}")]
        public async Task<IActionResult> GetAuthorsById(Guid authorId)
        {
            return Ok(await _mediator.Send(new GetAuthorsByIdQuery(authorId)));
        }

        [HttpPut]
        [Route("updateAuthor/{updatedAuthorId}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author updatedAuthor, Guid updatedAuthorId)
        {
            return Ok(await _mediator.Send(new UpdateAuthorByIdCommand(updatedAuthor, updatedAuthorId)));
        }

        [HttpDelete]
        [Route("deleteAuthor/{authorToDeleteId}")]
        public async Task<IActionResult> DeleteAuthor([FromBody] Guid authorToDeleteId)
        {
            return Ok(await _mediator.Send(new DeleteAuthorByIdCommand(authorToDeleteId)));
        }
    }
}
