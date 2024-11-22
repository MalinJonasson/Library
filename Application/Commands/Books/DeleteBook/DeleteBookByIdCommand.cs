using Domain.Models;
using MediatR;

namespace Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommand : IRequest<Book>
    {
        public DeleteBookByIdCommand(Guid bookId)
        {
            Id = bookId;
        }

        public Guid Id { get; }
    }
}
