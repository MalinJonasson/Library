using Domain.Models;
using MediatR;

namespace Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommand : IRequest<Book>
    {
        public UpdateBookByIdCommand(Book updatedBook, Guid id)
        {
            UpdatedBook = updatedBook;
            Id = id;
        }

        public Book UpdatedBook { get; }
        public Guid Id { get; }
    }
}
