using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.UpdateAuthor
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
            Author authorToUpdate = _fakeDatabase.Authors.FirstOrDefault(author => author.Id == request.Id)!;

            authorToUpdate.Name = request.UpdatedAuthor.Name;

            return Task.FromResult(authorToUpdate);
        }
    }
}
