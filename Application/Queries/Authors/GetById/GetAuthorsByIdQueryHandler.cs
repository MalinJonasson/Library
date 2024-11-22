using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Authors.GetById
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
            if (request == null || request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Author wantedAuthor = _fakeDatabase.Authors.FirstOrDefault(author => author.Id == request.Id)!;
            return Task.FromResult(wantedAuthor);
        }
    }
}
