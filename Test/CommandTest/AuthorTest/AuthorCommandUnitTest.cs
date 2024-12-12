using Microsoft.EntityFrameworkCore;
using Application.Commands.Authors.AddAuthor;
using Application.Commands.Authors.UpdateAuthor;
using Application.Commands.Authors.DeleteAuthor;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Authors.GetAll;
using Application.Queries.Authors.GetById;

namespace Test.CommandTest.AuthorTest
{
    public class AuthorCommandHandlerTests
    {
        private RealDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<RealDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new RealDatabase(options);
        }

        [Fact]
        public async Task Handle_ShouldAddAuthor_WhenValidRequest()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new AddAuthorCommandHandler(authorRepository);

            var newAuthor = new Author { Name = "Test Author" };
            var addAuthorCommand = new AddAuthorCommand(newAuthor);

            // Act
            var result = await handler.Handle(addAuthorCommand, CancellationToken.None);

            // Assert
            var addedAuthor = database.Authors.FirstOrDefault(a => a.Name == "Test Author");
            Assert.NotNull(addedAuthor);
            Assert.Equal("Test Author", addedAuthor.Name);
            Assert.True(result.IsSuccess);
            Assert.Equal("Author added successfully.", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAuthor_WhenValidRequest()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new UpdateAuthorByIdCommandHandler(authorRepository);

            var authorId = Guid.NewGuid();
            var existingAuthor = new Author { Id = authorId, Name = "Old Name" };
            database.Authors.Add(existingAuthor);
            await database.SaveChangesAsync();

            var updatedAuthor = new Author { Id = authorId, Name = "Updated Name" };
            var updateAuthorCommand = new UpdateAuthorByIdCommand(updatedAuthor, authorId);

            // Act
            var result = await handler.Handle(updateAuthorCommand, CancellationToken.None);

            // Assert
            var updatedDbAuthor = database.Authors.FirstOrDefault(a => a.Id == authorId);
            Assert.NotNull(updatedDbAuthor);
            Assert.Equal("Updated Name", updatedDbAuthor.Name);
            Assert.True(result.IsSuccess);
            Assert.Equal("Updated Name", result.Data.Name);
        }

        [Fact]
        public async Task Handle_ShouldDeleteAuthor_WhenValidId()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new DeleteAuthorByIdCommandHandler(authorRepository);

            var authorId = Guid.NewGuid();
            var existingAuthor = new Author { Id = authorId, Name = "Test Author" };
            database.Authors.Add(existingAuthor);
            await database.SaveChangesAsync();

            var deleteAuthorCommand = new DeleteAuthorByIdCommand(authorId);

            // Act
            var result = await handler.Handle(deleteAuthorCommand, CancellationToken.None);

            // Assert
            var deletedAuthor = database.Authors.FirstOrDefault(a => a.Id == authorId);
            Assert.Null(deletedAuthor); 
            Assert.True(result.IsSuccess);
            Assert.Equal("Author deleted successfully.", result.Message);
        }

    }
}
