using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorByIdCommand : IRequest<OperationResult<bool>>
    {
        public DeleteAuthorByIdCommand(Guid authorId)
        {
            Id = authorId;
        }

        public Guid Id { get; }
    }
}
