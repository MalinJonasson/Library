using Domain.Models;
using MediatR;

namespace Application.Commands.AddAuthor
{
    public class AddAuthorCommand : IRequest<Author>
    {
        public AddAuthorCommand(Author newAuthor)
        {
            NewAuthor = newAuthor;
        }

        public Author NewAuthor { get; }
    }
}
