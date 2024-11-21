using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.UpdateAuthor
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
