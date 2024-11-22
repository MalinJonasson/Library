using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAllAuthorsQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<List<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            List<Author> allAuthorsFromFakeDatabase = _fakeDatabase.Authors;

            // Nullcheck för att hantera om _fakeDatabase.Authors är null
            if (allAuthorsFromFakeDatabase == null || !allAuthorsFromFakeDatabase.Any())
            {
                throw new ArgumentException("Authorlist is empty or null");
            }

            return Task.FromResult(allAuthorsFromFakeDatabase);
        }

    }
}
