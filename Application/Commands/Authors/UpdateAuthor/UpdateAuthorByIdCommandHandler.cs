using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommandHandler : IRequestHandler<UpdateAuthorByIdCommand, Author>
    {

        private readonly FakeDatabase _fakeDatabase;


        public UpdateAuthorByIdCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Author> Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty ||
                string.IsNullOrWhiteSpace(request.UpdatedAuthor.Name))
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Author authorToUpdate = _fakeDatabase.Authors.FirstOrDefault(author => author.Id == request.Id)!;

            if (authorToUpdate != null && !string.IsNullOrWhiteSpace(request.UpdatedAuthor.Name))
            {
                authorToUpdate.Name = request.UpdatedAuthor.Name;
                return Task.FromResult(authorToUpdate);
            }

            throw new ArgumentNullException("Author not found or name cannot be empty");

        }
    }
}
