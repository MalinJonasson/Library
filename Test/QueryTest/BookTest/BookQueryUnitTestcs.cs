using Application.Queries.Books.GetAll;
using Application.Queries.Books.GetById;
using Domain.Models;

namespace Test.QueryTest.BookTest
{
    public class BookQueryUnitTestcs
    {
      // private FakeDatabase _fakeDatabase;
      // private GetAllBooksQueryHandler _getAllBooksQueryHandler;
      // private GetBookByIdQueryHandler _getBookByIdQueryHandler;
      //
      // [SetUp]
      // public void SetUp()
      // {
      //     _fakeDatabase = new FakeDatabase();
      //     _getAllBooksQueryHandler = new GetAllBooksQueryHandler(_fakeDatabase);
      //     _getBookByIdQueryHandler = new GetBookByIdQueryHandler(_fakeDatabase);
      // }
      //
      // [Test]
      // public async Task Handle_ShouldGetAllBooksFromFakeDatabaseAndReturnBooks()
      // {
      //     // Arrange
      //     var book1 = new Book { Id = Guid.NewGuid(), Title = "Book One" };
      //     var book2 = new Book { Id = Guid.NewGuid(), Title = "Book Two" };
      //     _fakeDatabase.Books.Add(book1);
      //     _fakeDatabase.Books.Add(book2);
      //
      //     var query = new GetAllBooksQuery();
      //
      //     // Act
      //     var result = await _getAllBooksQueryHandler.Handle(query, CancellationToken.None);
      //
      //     // Assert
      //     Assert.IsNotNull(result);
      //     Assert.AreEqual(7, result.Count);
      //     Assert.Contains(book1, result);
      //     Assert.Contains(book2, result);
      //
      // }
      //
      // [Test]
      // public async Task Handle_ShouldGetBookByIdFromFakeDatabaseAndReturnBook()
      // {
      //     // Arrange
      //     var bookToReturnId = new Guid("fa7c2886-a981-43dc-9acb-666dcf9025e3");
      //     var bookToReturn = new Book { Id = bookToReturnId, Title = "Test Book" };
      //     _fakeDatabase.Books.Add(bookToReturn);
      //
      //     var request = new GetBookByIdQuery(bookToReturnId);
      //
      //     // Act
      //     var result = await _getBookByIdQueryHandler.Handle(request, CancellationToken.None);
      //
      //     // Assert
      //     Assert.IsNotNull(result);
      //     Assert.AreEqual(bookToReturnId, result.Id);
      // }
      //
      // // [Test]
      // // public async Task Handle_ShouldReturnBookWithAuthor_WhenBookHasValidAuthorId()
      // // {
      // //     // Arrange
      // //     var bookId = Guid.NewGuid();
      // //     var authorId = Guid.NewGuid();
      // //
      // //     var author = new Author
      // //     {
      // //         Id = authorId,
      // //         Name = "Test Author"
      // //     };
      // //     _fakeDatabase.Authors.Add(author);
      // //
      // //     var book = new Book
      // //     {
      // //         Id = bookId,
      // //         Title = "Test Book",
      // //         Description = "Test Description",
      // //         AuthorId = authorId
      // //     };
      // //     _fakeDatabase.Books.Add(book);
      // //
      // //     var request = new GetBookByIdQuery(bookId);
      // //
      // //     // Act
      // //     var result = await _getBookByIdQueryHandler.Handle(request, CancellationToken.None);
      // //
      // //     // Assert
      // //     Assert.IsNotNull(result);
      // //     Assert.AreEqual(bookId, result.Id);
      // //     Assert.AreEqual(authorId, result.AuthorId);
      // //     Assert.IsNotNull(result.Author);
      // //     Assert.AreEqual(author.Name, result.Author.Name);
      // // }
      //
    } //

}

