using Application.Commands.Books.AddBook;
using Application.Commands.Books.DeleteBook;
using Application.Commands.Books.UpdateBook;
using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Books.GetAll;
using Application.Queries.Books.GetById;
using Domain.Models;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test.IntergrationsTest.BookIntergrationsTest
{
    public class BookIntegrationTests
    {
        private readonly IMediator _mediator;
        private readonly IBookRepository _bookRepositoryMock;

        public BookIntegrationTests()
        {
            _bookRepositoryMock = A.Fake<IBookRepository>();

            var services = new ServiceCollection();

            services.AddSingleton(_bookRepositoryMock);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllBooksQueryHandler>());

            var serviceProvider = services.BuildServiceProvider();

            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task AddBookCommand_ShouldAddBookToRepository()
        {
            // Arrange
            var newBook = new Book {Title = "Test Book", Description = "Test Description" };

            A.CallTo(() => _bookRepositoryMock.AddBook(A<Book>.That.Matches(book => book.Title == newBook.Title && book.Description == newBook.Description)))
                .Returns(Task.FromResult(newBook));

            // Act
            var result = await _mediator.Send(new AddBookCommand(newBook));

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(newBook.Title, result.Data.Title);
            Assert.Equal(newBook.Description, result.Data.Description);
            A.CallTo(() => _bookRepositoryMock.AddBook(A<Book>.That.Matches(book => book.Title == newBook.Title && book.Description == newBook.Description)))
                .MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task GetAllBooksQuery_ShouldCallRepositoryAndReturnBooks()
        {
            // Arrange
            var bookList = new List<Book>
            {
            new Book { Id = Guid.NewGuid(), Title = "Book 1" },
            new Book { Id = Guid.NewGuid(), Title = "Book 2" }
            };
            A.CallTo(() => _bookRepositoryMock.GetAllBooks())
                .Returns(Task.FromResult(bookList));

            // Act
            var result = await _mediator.Send(new GetAllBooksQuery());

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            A.CallTo(() => _bookRepositoryMock.GetAllBooks()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetBookByIdQuery_ShouldReturnCorrectBook()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Title = "Book 1" };
            A.CallTo(() => _bookRepositoryMock.GetBookById(bookId))
                .Returns(Task.FromResult(book));

            // Act
            var result = await _mediator.Send(new GetBookByIdQuery(bookId));

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(bookId, result.Data.Id);
            Assert.Equal(book.Title, result.Data.Title);
            A.CallTo(() => _bookRepositoryMock.GetBookById(bookId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateBookByIdCommand_ShouldUpdateBookAndReturnUpdatedBook()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var updatedBook = new Book { Id = bookId, Title = "Updated Title", Description = "Updated Description" };

            A.CallTo(() => _bookRepositoryMock.GetBookById(bookId))
                .Returns(Task.FromResult(new Book { Id = bookId, Title = "Old Title", Description = "Old Description" }));

            A.CallTo(() => _bookRepositoryMock.UpdateBookById(bookId, A<Book>.That.Matches(b => b.Title == "Updated Title" && b.Description == "Updated Description")))
                .Returns(Task.FromResult(updatedBook));

            var command = new UpdateBookByIdCommand(updatedBook, bookId);

            // Act
            var result = await _mediator.Send(command);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(bookId, result.Data.Id);
            Assert.Equal(updatedBook.Title, result.Data.Title);
            Assert.Equal(updatedBook.Description, result.Data.Description); 

            A.CallTo(() => _bookRepositoryMock.UpdateBookById(bookId, A<Book>.That.Matches(b => b.Title == "Updated Title" && b.Description == "Updated Description")))
                .MustHaveHappenedOnceExactly();

        }

        [Fact]
        public async Task DeleteBookByIdCommand_ShouldDeleteBookAndReturnIt()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var bookToDelete = new Book { Id = bookId, Title = "Book to delete" };

            A.CallTo(() => _bookRepositoryMock.DeleteBookById(bookId))
                .Returns(Task.FromResult(bookToDelete));

            // Act
            var result = await _mediator.Send(new DeleteBookByIdCommand(bookId));

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Book deleted successfully.", result.Message);

            A.CallTo(() => _bookRepositoryMock.DeleteBookById(bookId)).MustHaveHappenedOnceExactly();
        }
    }

}