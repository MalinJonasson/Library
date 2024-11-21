using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand, Author>
    {
        private readonly FakeDatabase _fakeDatabase;


        public DeleteAuthorByIdCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }


        public Task<Author> Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            Author? authorToDelete = _fakeDatabase.Authors.FirstOrDefault(author => author.Id == request.Id);

            if (authorToDelete == null)
            {
                return Task.FromResult<Author>(null!);
            }

            _fakeDatabase.Authors.Remove(authorToDelete);

            return Task.FromResult(authorToDelete);
        }
    }
}