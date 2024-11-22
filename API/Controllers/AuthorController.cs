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
        //CRUD GET UPDATE/PUT/PATCH POST DELETE

        internal readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("addNewAuthor")]
        public async void AddNewAuthor([FromBody] Author authorToAdd)
        {
            await _mediator.Send(new AddAuthorCommand(authorToAdd));
        }

        [HttpGet]
        [Route("getAllAuthors")]
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

        [Authorize]
        [HttpPut]
        [Route("updateAuthor/{updatedAuthorId}")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author updatedAuthor, Guid updatedAuthorId)
        {
            return Ok(await _mediator.Send(new UpdateAuthorByIdCommand(updatedAuthor, updatedAuthorId)));
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteAuthor/{authorToDeleteId}")]
        public async Task<IActionResult> DeleteAuthor([FromBody] Guid authorToDeleteId)
        {
            return Ok(await _mediator.Send(new DeleteAuthorByIdCommand(authorToDeleteId)));
        }
    }
}
