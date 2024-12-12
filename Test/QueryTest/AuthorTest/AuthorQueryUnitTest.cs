using Application.Queries.Authors.GetAll;
using Application.Queries.Authors.GetById;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test.QueryTest.AuthorTest
{
    public class AuthorQueryUnitTest
    {
        private RealDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<RealDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new RealDatabase(options);
        }

        [Fact]
        public async Task Handle_ShouldReturnAuthors_WhenAuthorsExist()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new GetAllAuthorsQueryHandler(authorRepository);

            database.Authors.Add(new Author { Id = Guid.NewGuid(), Name = "Test Author1" });
            database.Authors.Add(new Author { Id = Guid.NewGuid(), Name = "Test Author2" });
            await database.SaveChangesAsync();

            var getAllAuthorsQuery = new GetAllAuthorsQuery();

            // Act
            var result = await handler.Handle(getAllAuthorsQuery, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Authors retrieved successfully.", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnAuthor_WhenAuthorExists()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new GetAuthorsByIdQueryHandler(authorRepository);

            var authorId = Guid.NewGuid();
            var author = new Author { Id = authorId, Name = "Test Author" };
            database.Authors.Add(author);
            await database.SaveChangesAsync();

            var getAuthorsByIdQuery = new GetAuthorsByIdQuery(authorId);

            // Act
            var result = await handler.Handle(getAuthorsByIdQuery, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Test Author", result.Data.Name);
            Assert.Equal("Author retrieved successfully.", result.Message);
        }
    }
}
