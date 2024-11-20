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

        [SetUp]
        public void SetUp()
        {
            _fakeDatabase = new FakeDatabase();
            _addAuthorCommandHandler = new AddAuthorCommandHandler(_fakeDatabase);
            _deleteAuthorCommandHandler = new DeleteAuthorByIdCommandHandler(_fakeDatabase);
            _updateAuthorCommandHandler = new UpdateAuthorByIdCommandHandler(_fakeDatabase);
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


    }
}
