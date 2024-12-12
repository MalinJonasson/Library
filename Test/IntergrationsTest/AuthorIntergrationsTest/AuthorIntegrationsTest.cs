using Application.Commands.Authors.AddAuthor;
using Application.Commands.Authors.DeleteAuthor;
using Application.Commands.Authors.UpdateAuthor;
using Application.Commands.Books.AddBook;
using Application.Commands.Books.DeleteBook;
using Application.Commands.Books.UpdateBook;
using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Authors.GetAll;
using Application.Queries.Authors.GetById;
using Application.Queries.Books.GetAll;
using Application.Queries.Books.GetById;
using Domain.Models;
using FakeItEasy;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test.IntergrationsTest.AuthorIntergrationsTest
{
    public class AuthorIntegrationsTest
    {
        private readonly IMediator _mediator;
        private readonly IAuthorRepository _authorRepositoryMock;

        public AuthorIntegrationsTest()
        {
            // Mocka BookRepository med FakeItEasy
            _authorRepositoryMock = A.Fake<IAuthorRepository>();

            // Setup Dependency Injection och MediatR
            var services = new ServiceCollection();

            // Registrera IBookRepository som mock-implementation
            services.AddSingleton(_authorRepositoryMock);

            // Registrera MediatR och dess handlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllAuthorsQueryHandler>());

            // Bygg DI-container
            var serviceProvider = services.BuildServiceProvider();

            // Hämta MediatR via service provider
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task AddAuthorCommand_ShouldAddAuthorToRepository()
        {
            // Arrange
            var newAuthor = new Author { Name = "Test Author"};

            A.CallTo(() => _authorRepositoryMock.AddAuthor(A<Author>.That.Matches(author => author.Name == newAuthor.Name)))
                .Returns(Task.FromResult(newAuthor));

            // Act
            var result = await _mediator.Send(new AddAuthorCommand(newAuthor));

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(newAuthor.Name, result.Data.Name);

            // Kontrollera att AddBook anropades exakt en gång
            A.CallTo(() => _authorRepositoryMock.AddAuthor(A<Author>.That.Matches(author => author.Name == newAuthor.Name)))
                .MustHaveHappenedOnceExactly();
        }


        [Fact]
        public async Task GetAllAuthorQuery_ShouldCallRepositoryAndReturnAuthors()
        {
            // Arrange
            var authorList = new List<Author>
            {
            new Author { Id = Guid.NewGuid(), Name = "Author 1" },
            new Author { Id = Guid.NewGuid(), Name = "Author 2" }
            };
            A.CallTo(() => _authorRepositoryMock.GetAllAuthors())
                .Returns(Task.FromResult(authorList));

            // Act
            var result = await _mediator.Send(new GetAllAuthorsQuery());

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            A.CallTo(() => _authorRepositoryMock.GetAllAuthors()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetAuthorByIdQuery_ShouldReturnCorrectAuthor()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var author = new Author { Id = authorId, Name = "Author 1" };
            A.CallTo(() => _authorRepositoryMock.GetAuthorById(authorId))
                .Returns(Task.FromResult(author));

            // Act
            var result = await _mediator.Send(new GetAuthorsByIdQuery(authorId));

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(authorId, result.Data.Id);
            Assert.Equal(author.Name, result.Data.Name);
            A.CallTo(() => _authorRepositoryMock.GetAuthorById(authorId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateAuhtorByIdCommand_ShouldUpdateBookAndReturnUpdatedAuthor()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var updatedAuthor = new Author { Id = authorId, Name = "Updated Author" };

            A.CallTo(() => _authorRepositoryMock.GetAuthorById(authorId))
                .Returns(Task.FromResult(new Author { Id = authorId, Name = "Old Name" }));

            A.CallTo(() => _authorRepositoryMock.UpdateAuthorById(authorId, A<Author>.That.Matches(a => a.Name == "Updated Author")))
                .Returns(Task.FromResult(updatedAuthor));

            var command = new UpdateAuthorByIdCommand(updatedAuthor, authorId);

            // Act
            var result = await _mediator.Send(command);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(authorId, result.Data.Id);
            Assert.Equal(updatedAuthor.Name, result.Data.Name);

            A.CallTo(() => _authorRepositoryMock.UpdateAuthorById(authorId, A<Author>.That.Matches(a => a.Name == "Updated Author")))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteAuthorByIdCommand_ShouldDeleteAuthorAndReturnIt()
        {
            // Arrange
            var auhtorId = Guid.NewGuid();
            var authorToDelete = new Author { Id = auhtorId, Name = "Author to delete" };

            A.CallTo(() => _authorRepositoryMock.DeleteAuthorById(auhtorId))
                .Returns(Task.FromResult(authorToDelete));

            // Act
            var result = await _mediator.Send(new DeleteAuthorByIdCommand(auhtorId));

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Author deleted successfully.", result.Message);

            A.CallTo(() => _authorRepositoryMock.DeleteAuthorById(auhtorId)).MustHaveHappenedOnceExactly();
        }
    }
}
