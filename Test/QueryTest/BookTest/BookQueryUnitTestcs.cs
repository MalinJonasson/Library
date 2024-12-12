using Application.Queries.Books.GetAll;
using Application.Queries.Books.GetById;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Test.QueryTest.BookTest
{
    public class BookQueryUnitTestcs
    {
        private RealDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<RealDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new RealDatabase(options);
        }

        [Fact]
        public async Task Handle_ShouldReturnBooks_WhenBooksExist()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new GetAllBooksQueryHandler(bookRepository);

            database.Books.Add(new Book { Id = Guid.NewGuid(), Title = "Test Book1", Description = "Description1" });
            database.Books.Add(new Book { Id = Guid.NewGuid(), Title = "Test Book2", Description = "Description2" });
            await database.SaveChangesAsync();

            var getAllBooksQuery = new GetAllBooksQuery();

            // Act
            var result = await handler.Handle(getAllBooksQuery, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Books retrieved successfully.", result.Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new GetBookByIdQueryHandler(bookRepository);

            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Title = "Test Book", Description = "Test Description" };
            database.Books.Add(book);
            await database.SaveChangesAsync();

            var getBookByIdQuery = new GetBookByIdQuery(bookId);

            // Act
            var result = await handler.Handle(getBookByIdQuery, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Test Book", result.Data.Title);
            Assert.Equal("Test Description", result.Data.Description);
            Assert.Equal("Book retrieved successfully.", result.Message);
        }
    } 

}

