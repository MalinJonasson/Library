using Application.Commands.Books.AddBook;
using Application.Commands.Books.DeleteBook;
using Application.Commands.Books.UpdateBook;
using Application.Queries.Books.GetAll;
using Application.Queries.Books.GetById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        //CRUD GET UPDATE/PUT/PATCH POST DELETE

        internal readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [Route("addNewBook")]
        public async void AddNewBook([FromBody] Book bookToAdd)
        {
            await _mediator.Send(new AddBookCommand(bookToAdd));
        }

        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(await _mediator.Send(new GetAllBooksQuery()));
        }

        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> GetBookById(Guid bookId)
        {
            return Ok(await _mediator.Send(new GetBookByIdQuery(bookId)));
        }

        [Authorize]
        [HttpPut]
        [Route("updateBook/{updatedBookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] Book updatedBook, Guid updatedBookId)
        {
            return Ok(await _mediator.Send(new UpdateBookByIdCommand(updatedBook, updatedBookId)));
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteBook/{bookToDeleteId}")]
        public async Task<IActionResult> DeleteBook([FromBody] Guid bookToDeleteId)
        {
            return Ok(await _mediator.Send(new DeleteBookByIdCommand(bookToDeleteId)));
        }

    }
}
