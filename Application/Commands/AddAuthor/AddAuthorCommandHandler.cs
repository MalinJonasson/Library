using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public AddAuthorCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        Task<Author> IRequestHandler<AddAuthorCommand, Author>.Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            Author authorToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewAuthor.Name
            };

            _fakeDatabase.Authors.Add(authorToCreate);

            return Task.FromResult(authorToCreate);
        }
    }
}
