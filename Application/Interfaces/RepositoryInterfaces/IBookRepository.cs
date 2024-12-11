using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBookById(Guid id, Book book);
        Task<Book> DeleteBookById(Guid id);
        Task<Book> GetBookById(Guid id);
        Task<List<Book>> GetAllBooks();
    }
}
