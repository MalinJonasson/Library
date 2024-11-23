using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Books.GetAll
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAllBooksQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            List<Book> allBooksFromFakeDatabase = _fakeDatabase.Books;

            if(allBooksFromFakeDatabase == null || !allBooksFromFakeDatabase.Any())
            {
                throw new ArgumentException("Booklist is empty or null");
            }

            foreach (var book in allBooksFromFakeDatabase)
            {
                var authorToGet = _fakeDatabase.Authors.FirstOrDefault(a => a.Id == book.AuthorId);
                if (authorToGet != null)
                { 
                    book.Author = authorToGet;
                }
            }
            return Task.FromResult(allBooksFromFakeDatabase);
        }
    }
}
