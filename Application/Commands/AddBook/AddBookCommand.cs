using Domain.Models;
using MediatR;

namespace Application.Commands.AddBook
{
    public class AddBookCommand : IRequest <Book>
    {
        public AddBookCommand(Book newBook)
        {
            NewBook = newBook;
        }

        public Book NewBook { get; }
    }
}
