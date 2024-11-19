using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.GetAllById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetBookByIdQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            Book wantedBook = _fakeDatabase.Books.FirstOrDefault(book => book.Id == request.Id)!;
            return Task.FromResult(wantedBook);
        }
    }
}
