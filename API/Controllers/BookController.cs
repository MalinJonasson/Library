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
        private readonly IMediator _mediator;
        private readonly ILogger<BookController> _logger;

        public BookController(IMediator mediator, ILogger<BookController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        [Route("addNewBook")]
        public async Task<IActionResult> AddNewBook([FromBody] Book bookToAdd)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for AddNewBook.");
                return BadRequest(ModelState);
            }

            try
            {
                var addBookOperationResult = await _mediator.Send(new AddBookCommand(bookToAdd));

                if (addBookOperationResult.IsSuccess)
                {
                    _logger.LogInformation("Successfully added a new book with ID: {BookId}", addBookOperationResult.Data.Id);
                    return Ok(new
                    {
                        message = addBookOperationResult.Message,
                        book = addBookOperationResult.Data
                    });
                }

                _logger.LogWarning("Failed to add book: {Error}", addBookOperationResult.Message);
                return BadRequest(new { message = addBookOperationResult.Message, errors = addBookOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in AddNewBook.");
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpGet]
        [Route("getAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var getAllBooksOperationResult = await _mediator.Send(new GetAllBooksQuery());

                if (getAllBooksOperationResult.IsSuccess)
                {
                    return Ok(getAllBooksOperationResult.Data);
                }

                _logger.LogWarning("Failed to retrieve books: {Error}", getAllBooksOperationResult.Message);
                return BadRequest(new { message = getAllBooksOperationResult.Message, errors = getAllBooksOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllBooks.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> GetBookById(Guid bookId)
        {
            try
            {
                var getBookByIdOperationResult = await _mediator.Send(new GetBookByIdQuery(bookId));

                if (getBookByIdOperationResult.IsSuccess)
                {
                    return Ok(getBookByIdOperationResult.Data);
                }

                _logger.LogWarning("Book with ID: {BookId} not found.", bookId);
                return NotFound(new { message = getBookByIdOperationResult.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetBookById.");
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [Authorize]
        [HttpPut]
        [Route("updateBook/{updatedBookId}")]
        public async Task<IActionResult> UpdateBook([FromBody] Book updatedBook, Guid updatedBookId)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateBook.");
                return BadRequest(ModelState);
            }

            try
            {
                var updateBookByIdOperationResult = await _mediator.Send(new UpdateBookByIdCommand(updatedBook, updatedBookId));

                if (updateBookByIdOperationResult.IsSuccess)
                {
                    return Ok(new { message = updateBookByIdOperationResult.Message });
                }

                _logger.LogWarning("Failed to update book: {Error}", updateBookByIdOperationResult.Message);
                return BadRequest(new { message = updateBookByIdOperationResult.Message, errors = updateBookByIdOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in UpdateBook.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteBook/{bookToDeleteId}")]
        public async Task<IActionResult> DeleteBook([FromBody] Guid bookToDeleteId)
        {
            try
            {
                var deleteBookByIdOperationResult = await _mediator.Send(new DeleteBookByIdCommand(bookToDeleteId));

                if (deleteBookByIdOperationResult.IsSuccess)
                {
                    return Ok(new { message = deleteBookByIdOperationResult.Message });
                }

                _logger.LogWarning("Failed to delete book: {Error}", deleteBookByIdOperationResult.Message);
                return BadRequest(new { message = deleteBookByIdOperationResult.Message, errors = deleteBookByIdOperationResult.ErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in DeleteBook.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
}
