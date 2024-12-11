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
        public async Task<Book> AddBook(Book book)
        {
            _realDatabase.Books.Add(book);
            _realDatabase.SaveChanges();
            return book;
        }

        public async Task<Book> DeleteBookById(Guid id)
        {
            Book bookToDelete = _realDatabase.Books.FirstOrDefault(a => a.Id == id);

            _realDatabase.Books.Remove(bookToDelete);
            await _realDatabase.SaveChangesAsync();

            return bookToDelete;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await Task.FromResult(_realDatabase.Books.ToList());
        }

        public async Task<Book> GetBookById(Guid id)
        {
            Book book = _realDatabase.Books.FirstOrDefault(a => a.Id == id);

            return await Task.FromResult(book);
        }

        public async Task<Book> UpdateBookById(Guid id, Book book)
        {
            Book bookToUpdate = _realDatabase.Books.FirstOrDefault(a => a.Id == id);

            if (bookToUpdate == null)
            {
                throw new KeyNotFoundException($"Author with ID {id} was not found.");
            }

            bookToUpdate.Title = book.Title;

            await _realDatabase.SaveChangesAsync();

            return bookToUpdate;
        }
    }
}
