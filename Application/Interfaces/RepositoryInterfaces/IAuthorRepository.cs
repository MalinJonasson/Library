using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthorById(Guid id, Author author);
        Task<Author> DeleteAuthorById(Guid id);
        Task<Author> GetAuthorById(Guid id);
        Task<List<Author>> GetAllAuthors();
    }
}
