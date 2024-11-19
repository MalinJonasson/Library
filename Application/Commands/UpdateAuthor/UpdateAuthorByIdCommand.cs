using Domain.Models;
using MediatR;

namespace Application.Commands.UpdateAuthor
{
    public class UpdateAuthorByIdCommand : IRequest<Author>
    {
        public UpdateAuthorByIdCommand(Author updatedAuthor, Guid id)
        {
            UpdatedAuthor = updatedAuthor;
            Id = id;
        }

        public Author UpdatedAuthor { get; }
        public Guid Id { get; }
    }
}
