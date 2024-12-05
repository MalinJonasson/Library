using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RealDatabase _realDatabase;

        public BookRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }
        public Task<Book> AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<Book> DeleteBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> UpdateBookById(Guid id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
