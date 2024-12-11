using Application.Commands.Authors.AddAuthor;
using Application.Commands.Authors.DeleteAuthor;
using Application.Commands.Authors.UpdateAuthor;
using Domain.Models;
namespace Test.CommandTest.AuthorTest
{
    [TestFixture]
    public class AuthorCommandUnitTest
    {
        //private FakeDatabase _fakeDatabase;
        private AddAuthorCommandHandler _addAuthorCommandHandler;
        private DeleteAuthorByIdCommandHandler _deleteAuthorCommandHandler;
        private UpdateAuthorByIdCommandHandler _updateAuthorCommandHandler;

      // [SetUp]
      // public void SetUp()
      // {
      //     //_fakeDatabase = new FakeDatabase();
      //     _addAuthorCommandHandler = new AddAuthorCommandHandler(_fakeDatabase);
      //     _deleteAuthorCommandHandler = new DeleteAuthorByIdCommandHandler(_fakeDatabase);
      //     _updateAuthorCommandHandler = new UpdateAuthorByIdCommandHandler(_fakeDatabase);
      // }
      //
      // [Test]
      // public async Task Handle_ShouldAddAuthorToFakeDatabaseAndReturnAuthor()
      // {
      //     // Arrange
      //     var newAuthor = new Author { Name = "Test Author" };
      //     var request = new AddAuthorCommand(newAuthor);
      //
      //     // Act
      //     var result = await _addAuthorCommandHandler.Handle(request, CancellationToken.None);
      //
      //     // Assert
      //     Assert.IsNotNull(result, "The returned author should not be null.");
      //     Assert.AreEqual(newAuthor.Name, result.Name, "The author's name should match the input.");
      //     Assert.IsTrue(_fakeDatabase.Authors.Contains(result), "The author should be added to the database.");
      // }
      //
      // [Test]
      // public async Task Handle_ShouldDeleteAuthorFromFakeDatabaseAndReturnAuthor()
      // {
      //     // Arrange
      //     var authorToDeleteId = new Guid("fa7c2886-a981-43dc-9acb-666dcf9025e3");
      //
      //     var authorToDelete = new Author { Id = authorToDeleteId, Name = "Test Author" };
      //     _fakeDatabase.Authors.Add(authorToDelete);
      //
      //     var request = new DeleteAuthorByIdCommand(authorToDeleteId);
      //     // Act
      //     var result = await _deleteAuthorCommandHandler.Handle(request, CancellationToken.None);
      //
      //     // Assert
      //     Assert.IsNotNull(result);
      //     Assert.AreEqual(authorToDeleteId, result.Id);
      //     Assert.IsFalse(_fakeDatabase.Authors.Any(a => a.Id == authorToDeleteId));
      //
      // }
      //
      //
      // [Test]
      // public async Task Handle_ShouldUpdateOldAuthorNameByIdFromFakeDatabaseAndReturnNewAuthorName()
      // {
      //     // Arrange
      //     var authorToUpdateId = Guid.NewGuid();
      //     var existingAuthor = new Author { Id = authorToUpdateId, Name = "Old Name" };
      //
      //     _fakeDatabase.Authors.Add(existingAuthor);
      //
      //     var updatedAuthor = new Author { Name = "Updated Name" };
      //     var request = new UpdateAuthorByIdCommand(updatedAuthor, authorToUpdateId);
      //     // Act
      //     var result = await _updateAuthorCommandHandler.Handle(request, CancellationToken.None);
      //
      //     // Assert
      //     Assert.IsNotNull(result, "The result should not be null.");
      //     Assert.AreEqual(authorToUpdateId, result.Id, "The author's ID should match the updated ID.");
      //     Assert.AreEqual(updatedAuthor.Name, result.Name, "The author's name should be updated.");
      //
      // }
    }
}