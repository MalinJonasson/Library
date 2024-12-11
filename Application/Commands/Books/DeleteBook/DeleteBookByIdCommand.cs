using Domain.Models;
using MediatR;

namespace Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommand : IRequest<OperationResult<bool>>
    {
        public DeleteBookByIdCommand(Guid bookIdToDelete)
        {
            Id = bookIdToDelete;
        }

        public Guid Id { get; }
    }
}
