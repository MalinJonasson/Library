using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly RealDatabase _realDatabase;

        public AuthorRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }
        public async Task<Author> AddAuthor(Author author)
        {
            _realDatabase.Authors.Add(author);
            _realDatabase.SaveChanges();
            return author;
        }

        public async Task<Author> DeleteAuthorById(Guid id)
        {
            Author authorToDelete = _realDatabase.Authors.FirstOrDefault(a => a.Id == id);

            _realDatabase.Authors.Remove(authorToDelete);
            await _realDatabase.SaveChangesAsync();

            return authorToDelete;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await Task.FromResult(_realDatabase.Authors.ToList());
        }

        public async Task<Author> GetAuthorById(Guid id)
        {
            Author author = _realDatabase.Authors.FirstOrDefault(a => a.Id == id);

            return await Task.FromResult(author);
        }

        public async Task<Author> UpdateAuthorById(Guid id, Author author)
        {
            Author authorToUpdate = _realDatabase.Authors.FirstOrDefault(a => a.Id == id);

            if (authorToUpdate == null)
            {
                throw new KeyNotFoundException($"Author with ID {id} was not found.");
            }

            authorToUpdate.Name = author.Name;

            await _realDatabase.SaveChangesAsync();

            return authorToUpdate;
        }
    }
}
