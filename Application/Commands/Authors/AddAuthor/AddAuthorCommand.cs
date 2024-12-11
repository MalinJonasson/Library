using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommand : IRequest<OperationResult<Author>>
    {
        public AddAuthorCommand(Author newAuthor)
        {
            NewAuthor = newAuthor;
        }
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(NewAuthor.Name);
        }
        public Author NewAuthor { get; }
    }
}
