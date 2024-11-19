using Application.Commands.AddBook;
using Application.Commands.DeleteBook;
using Application.Commands.UpdateBook;
using Application.Queries.GetAll;
using Application.Queries.GetAllById;
using Domain.Models;
using MediatR;

namespace Application
{
    public class BookMethods
    {
        private readonly IMediator _mediator;

        public BookMethods() { }

        public BookMethods(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Vallidering/felhantering
        public async void AddNewBook(Book book)
        {
            await _mediator.Send(new AddBookCommand(book));
        }

        public async void DeleteBook(Guid bookToDeleteId)
        {
            await _mediator.Send(new DeleteBookByIdCommand(bookToDeleteId));
        }

        public async void UpdateABook(Book updatedBook, Guid updatedBookId)
        {
            await _mediator.Send(new UpdateBookByIdCommand(updatedBook, updatedBookId));
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return (await _mediator.Send(new GetAllBooksQuery()));
        }

        public async void GetAllBooksById(Guid bookId)
        {
            await _mediator.Send(new GetBookByIdQuery(bookId));
        }
    }
}

