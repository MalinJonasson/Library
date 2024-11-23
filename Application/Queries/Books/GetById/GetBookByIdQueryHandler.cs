using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Books.GetById
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
            if (request == null || request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Book wantedBook = _fakeDatabase.Books.FirstOrDefault(book => book.Id == request.Id)!;

            var author = _fakeDatabase.Authors.FirstOrDefault(a => a.Id == wantedBook.AuthorId);
            if (author != null)
            {
                wantedBook.Author = author;
            }

            return Task.FromResult(wantedBook);
        }
    }
}
