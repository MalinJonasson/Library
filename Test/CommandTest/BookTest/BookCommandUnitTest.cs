using Application.Commands.Books.AddBook;
using Application.Commands.Books.DeleteBook;
using Application.Commands.Books.UpdateBook;
using Application.Queries.Books.GetAll;
using Application.Queries.Books.GetById;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test.CommandTest.BookTest
{
    public class BookCommandHandlerTests
    {
        private RealDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<RealDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new RealDatabase(options);
        }

        [Fact]
        public async Task Handle_ShouldAddBook_WhenValidRequest()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new AddBookCommandHandler(bookRepository);

            var newBook = new Book { Title = "Test Book", Description = "Test Description" };
            var addBookCommand = new AddBookCommand(newBook);

            // Act
            var result = await handler.Handle(addBookCommand, CancellationToken.None);

            // Assert
            var addedBook = database.Books.FirstOrDefault(b => b.Title == "Test Book");
            Assert.NotNull(addedBook);
            Assert.Equal("Test Book", addedBook.Title);
            Assert.Equal("Test Description", addedBook.Description);
            Assert.True(result.IsSuccess);
            Assert.Equal("Book added successfully.", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldUpdateBook_WhenValidRequest()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new UpdateBookByIdCommandHandler(bookRepository);

            var bookId = Guid.NewGuid();
            var existingBook = new Book { Id = bookId, Title = "Old Title", Description = "Old Description" };
            database.Books.Add(existingBook);
            await database.SaveChangesAsync();

            var updatedBook = new Book { Id = bookId, Title = "Updated Title", Description = "Updated Description" };
            var updateBookCommand = new UpdateBookByIdCommand(updatedBook, bookId);

            // Act
            var result = await handler.Handle(updateBookCommand, CancellationToken.None);

            // Assert
            var updatedDbBook = database.Books.FirstOrDefault(b => b.Id == bookId);
            Assert.NotNull(updatedDbBook);
            Assert.Equal("Updated Title", updatedDbBook.Title);
            Assert.Equal("Updated Description", updatedDbBook.Description);
            Assert.True(result.IsSuccess);
            Assert.Equal("Updated Title", result.Data.Title);
        }

        [Fact]
        public async Task Handle_ShouldDeleteBook_WhenValidId()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new DeleteBookByIdCommandHandler(bookRepository);

            var bookId = Guid.NewGuid();
            var existingBook = new Book { Id = bookId, Title = "Test Book", Description = "Test Description" };
            database.Books.Add(existingBook);
            await database.SaveChangesAsync();

            var deleteBookCommand = new DeleteBookByIdCommand(bookId);

            // Act
            var result = await handler.Handle(deleteBookCommand, CancellationToken.None);

            // Assert
            var deletedBook = database.Books.FirstOrDefault(b => b.Id == bookId);
            Assert.Null(deletedBook);
            Assert.True(result.IsSuccess);
            Assert.Equal("Book deleted successfully.", result.Message);
        }

    }
}
