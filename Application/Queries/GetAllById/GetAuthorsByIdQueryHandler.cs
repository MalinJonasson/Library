using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.GetAllById
{
    public class GetAuthorsByIdQueryHandler : IRequestHandler<GetAuthorsByIdQuery, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAuthorsByIdQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<Author> Handle(GetAuthorsByIdQuery request, CancellationToken cancellationToken)
        {
            Author wantedAuthor = _fakeDatabase.Authors.FirstOrDefault(author => author.Id == request.Id)!;
            return Task.FromResult(wantedAuthor);
        }
    }
}
