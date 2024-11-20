using Application.Commands.AddAuthor;
using Application.Commands.DeleteAuthor;
using Application.Commands.UpdateAuthor;
using Application.Queries.GetAll;
using Application.Queries.GetAllById;
using Domain.Models;
using Infrastructure.Database;

namespace Test.CommandTest.AuthorTest
{
    [TestFixture]
    public class AuthorCommandUnitTest
    {
        private FakeDatabase _fakeDatabase;
        private AddAuthorCommandHandler _addAuthorCommandHandler;
        private DeleteAuthorByIdCommandHandler _deleteAuthorCommandHandler;
        private UpdateAuthorByIdCommandHandler _updateAuthorCommandHandler;
        private GetAllAuthorsQueryHandler _getAllAuthorsQueryHandler;
        private GetAuthorsByIdQueryHandler _getAuthorsByIdQueryHandler;

        [SetUp]
        public void SetUp()
        {
            // Initialize FakeDatabase and handler before each test
            _fakeDatabase = new FakeDatabase();
            _addAuthorCommandHandler = new AddAuthorCommandHandler(_fakeDatabase);
            _deleteAuthorCommandHandler = new DeleteAuthorByIdCommandHandler(_fakeDatabase);
            _updateAuthorCommandHandler = new UpdateAuthorByIdCommandHandler(_fakeDatabase);
            _getAllAuthorsQueryHandler = new GetAllAuthorsQueryHandler(_fakeDatabase);
            _getAuthorsByIdQueryHandler = new GetAuthorsByIdQueryHandler(_fakeDatabase);
        }

        [Test]
        public async Task Handle_ShouldAddAuthorToFakeDatabaseAndReturnAuthor()
        {
            // Arrange
            var newAuthor = new Author { Name = "Test Author" };
            var request = new AddAuthorCommand(newAuthor);

            // Act
            var result = await _addAuthorCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "The returned author should not be null.");
            Assert.AreEqual(newAuthor.Name, result.Name, "The author's name should match the input.");
            Assert.IsTrue(_fakeDatabase.Authors.Contains(result), "The author should be added to the database.");
        }

        [Test]
        public async Task Handle_ShouldDeleteAuthorFromFakeDatabaseAndReturnAuthor()
        {
            // Arrange
            var authorToDeleteId = new Guid("fa7c2886-a981-43dc-9acb-666dcf9025e3");

            // Lägg till en författare i _fakeDatabase med det ID:t
            var authorToDelete = new Author { Id = authorToDeleteId, Name = "Test Author" };
            _fakeDatabase.Authors.Add(authorToDelete); // Lägg till i databasen

            // Skapa en instans av GetAuthorsByIdQuery med det ID:t
            var request = new DeleteAuthorByIdCommand(authorToDeleteId);
            // Act
            var result = await _deleteAuthorCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "The returned author should not be null."); // Författaren som togs bort ska returneras
            Assert.AreEqual(authorToDeleteId, result.Id, "The returned author's ID should match the deleted author's ID.");
            Assert.IsFalse(_fakeDatabase.Authors.Any(a => a.Id == authorToDeleteId), "The author should be removed from the database.");

        }


        [Test]
        public async Task Handle_ShouldUpdateOldAuthorNameByIdFromFakeDatabaseAndReturnNewAuthorName()
        {
            // Arrange
            var authorToUpdateId = Guid.NewGuid(); // Generera ett nytt GUID för den nya författaren
            var existingAuthor = new Author { Id = authorToUpdateId, Name = "Old Name" };

            _fakeDatabase.Authors.Add(existingAuthor); // Lägg till den skapade författaren i databasen

            var updatedAuthor = new Author { Name = "Updated Name" }; // Författaren ska uppdatera sitt namn
            var request = new UpdateAuthorByIdCommand(updatedAuthor, authorToUpdateId); // Skapa kommandot för uppdatering

            // Act
            var result = await _updateAuthorCommandHandler.Handle(request, CancellationToken.None); // Kör hanteraren

            // Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual(authorToUpdateId, result.Id, "The author's ID should match the updated ID.");
            Assert.AreEqual(updatedAuthor.Name, result.Name, "The author's name should be updated.");

        }

        [Test]
        public async Task Handle_ShouldGetAllAuthorsFromFakeDatabaseAndReturnAuthors()
        {
            // Arrange
            var author1 = new Author { Id = Guid.NewGuid(), Name = "Author One" };
            var author2 = new Author { Id = Guid.NewGuid(), Name = "Author Two" };
            _fakeDatabase.Authors.Add(author1);
            _fakeDatabase.Authors.Add(author2);

            var query = new GetAllAuthorsQuery();

            // Act
            var result = await _getAllAuthorsQueryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual(7, result.Count, "There should be 7 authors.");
            Assert.Contains(author1, result, "The first author should be in the result.");
            Assert.Contains(author2, result, "The second author should be in the result.");

        }

        [Test]
        public async Task Handle_ShouldGetAuthorsByIdFromFakeDatabaseAndReturnAuthor()
        {
            // Arrange
            var authorToReturnId = new Guid("fa7c2886-a981-43dc-9acb-666dcf9025e3");

            // Lägg till en författare i _fakeDatabase med det ID:t
            var authorToReturn = new Author { Id = authorToReturnId, Name = "Test Author" };
            _fakeDatabase.Authors.Add(authorToReturn); // Lägg till i databasen

            // Skapa en instans av GetAuthorsByIdQuery med det ID:t
            var request = new GetAuthorsByIdQuery(authorToReturnId);

            // Act
            var result = await _getAuthorsByIdQueryHandler.Handle(request, CancellationToken.None);  // Skicka queryn istället för ID:t direkt

            // Assert
            Assert.IsNotNull(result, "The returned author should not be null.");
            Assert.AreEqual(authorToReturnId, result.Id, "The returned author's ID should match the requested author's ID.");
        }

    }
}
