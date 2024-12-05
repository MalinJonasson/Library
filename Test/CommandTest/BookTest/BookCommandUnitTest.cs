using Application.Commands.Books.AddBook;
using Application.Commands.Books.DeleteBook;
using Application.Commands.Books.UpdateBook;
using Domain.Models;

namespace Test.CommandTest.BookTest
{
    public class BookCommandUnitTest
    {

       // private FakeDatabase _fakeDatabase;
       // private AddBookCommandHandler _addBookCommandHandler;
       // private DeleteBookByIdCommandHandler _deleteBookByIdCommandHandler;
       // private UpdateBookByIdCommandHandler _updateBookByIdCommandHandler;
       //
       // [SetUp]
       // public void SetUp()
       // {
       //     _fakeDatabase = new FakeDatabase();
       //     _addBookCommandHandler = new AddBookCommandHandler(_fakeDatabase);
       //     _deleteBookByIdCommandHandler = new DeleteBookByIdCommandHandler(_fakeDatabase);
       //     _updateBookByIdCommandHandler = new UpdateBookByIdCommandHandler(_fakeDatabase);
       // }
       //
       // [Test]
       // public async Task Handle_ShouldAddBookToFakeDatabaseAndReturnBookWithCorrectAuthor()
       // {
       //     // Arrange
       //     var author = _fakeDatabase.Authors.FirstOrDefault();
       //     var newBook = new Book { Title = "Test Book", Description = "Test description" };
       //     var request = new AddBookCommand(newBook);
       //
       //     // Act
       //     var result = await _addBookCommandHandler.Handle(request, CancellationToken.None);
       //
       //     // Assert
       //     Assert.IsNotNull(result);
       //     Assert.AreEqual(newBook.Title, result.Title);
       //     Assert.IsTrue(_fakeDatabase.Books.Contains(result));
       // }
       //
       //
       // [Test]
       // public async Task Handle_ShouldDeleteBookFromFakeDatabaseAndReturnBook()
       // {
       //     // Arrange
       //     var bookToDeleteId = new Guid("fa7c2886-a981-43dc-9acb-666dcf9025e3");
       //
       //     var bookToDelete = new Book { Id = bookToDeleteId, Title = "Test Book" };
       //     _fakeDatabase.Books.Add(bookToDelete);
       //
       //     var request = new DeleteBookByIdCommand(bookToDeleteId);
       //     // Act
       //     var result = await _deleteBookByIdCommandHandler.Handle(request, CancellationToken.None);
       //
       //     // Assert
       //     Assert.IsNotNull(result);
       //     Assert.AreEqual(bookToDeleteId, result.Id);
       //     Assert.IsFalse(_fakeDatabase.Books.Any(b => b.Id == bookToDeleteId));
       //
       // }
       //
       //
       // [Test]
       // public async Task Handle_ShouldUpdateBookTitleAndDescription()
       // {
       //     // Arrange
       //     var bookToUpdateId = Guid.NewGuid();
       //     var existingBook = new Book
       //     {
       //         Id = bookToUpdateId,
       //         Title = "Old Title",
       //         Description = "Old Description"
       //     };
       //     _fakeDatabase.Books.Add(existingBook);
       //
       //     var updatedBook = new Book
       //     {
       //         Title = "Updated Title",
       //         Description = "Updated Description"
       //     };
       //     var request = new UpdateBookByIdCommand(updatedBook, bookToUpdateId);
       //
       //     // Act
       //     var result = await _updateBookByIdCommandHandler.Handle(request, CancellationToken.None);
       //
       //     // Assert
       //     Assert.AreEqual(updatedBook.Title, result.Title);
       //     Assert.AreEqual(updatedBook.Description, result.Description);
       // }
       //
       // //[Test]
       // //public async Task Handle_ShouldUpdateAuthorOfBook()
       // //{
       // //    // Arrange
       // //    var bookToUpdateId = Guid.NewGuid();
       // //    var authorId = Guid.NewGuid();
       // //
       // //    var existingBook = new Book
       // //    {
       // //        Id = bookToUpdateId,
       // //        Title = "Old Title",
       // //        Description = "Old Description",
       // //    };
       // //    var existingAuthor = new Author
       // //    {
       // //        Id = authorId,
       // //        Name = "New Author"
       // //    };
       // //    _fakeDatabase.Books.Add(existingBook);
       // //    _fakeDatabase.Authors.Add(existingAuthor);
       // //
       // //    var updatedBook = new Book
       // //    {
       // //        Title = "Updated Title",
       // //        Description = "Updated Description",
       // //        AuthorId = authorId
       // //    };
       // //    var request = new UpdateBookByIdCommand(updatedBook, bookToUpdateId);
       // //
       // //    // Act
       // //    var result = await _updateBookByIdCommandHandler.Handle(request, CancellationToken.None);
       // //
       // //    // Assert
       // //    Assert.AreEqual(authorId, result.AuthorId);
       // //    Assert.AreEqual(existingAuthor, result.Author);
       // //}
       //
    }  //
}
