using Application.Dtos;
using Domain.Models;
using MediatR;

namespace Application.Commands.Books.AddBook
{
    public class AddBookCommand : IRequest<OperationResult<Book>>
    {
        public AddBookCommand(Book newBook)
        {
            NewBook = newBook;
        }

        public Book NewBook { get; }
    }
}
