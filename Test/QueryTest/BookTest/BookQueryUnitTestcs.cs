using Application.Queries.Books.GetAll;
using Application.Queries.Books.GetById;
using Domain.Models;
using Infrastructure.Database;

namespace Test.QueryTest.BookTest
{
    public class BookQueryUnitTestcs
    {
        private FakeDatabase _fakeDatabase;
        private GetAllBooksQueryHandler _getAllBooksQueryHandler;
        private GetBookByIdQueryHandler _getBookByIdQueryHandler;

        [SetUp]
        public void SetUp()
        {
            _fakeDatabase = new FakeDatabase();
            _getAllBooksQueryHandler = new GetAllBooksQueryHandler(_fakeDatabase);
            _getBookByIdQueryHandler = new GetBookByIdQueryHandler(_fakeDatabase);
        }

        [Test]
        public async Task Handle_ShouldGetAllBooksFromFakeDatabaseAndReturnBooks()
        {
            // Arrange
            var book1 = new Book { Id = Guid.NewGuid(), Title = "Book One" };
            var book2 = new Book { Id = Guid.NewGuid(), Title = "Book Two" };
            _fakeDatabase.Books.Add(book1);
            _fakeDatabase.Books.Add(book2);

            var query = new GetAllBooksQuery();

            // Act
            var result = await _getAllBooksQueryHandler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual(7, result.Count, "There should be 7 books.");
            Assert.Contains(book1, result, "The first book should be in the result.");
            Assert.Contains(book2, result, "The second book should be in the result.");

        }

        [Test]
        public async Task Handle_ShouldGetBookByIdFromFakeDatabaseAndReturnBook()
        {
            // Arrange
            var bookToReturnId = new Guid("fa7c2886-a981-43dc-9acb-666dcf9025e3");

            // Lägg till en boken i _fakeDatabase med det ID:t
            var bookToReturn = new Book { Id = bookToReturnId, Title = "Test Book" };
            _fakeDatabase.Books.Add(bookToReturn); // Lägg till i databasen

            // Skapa en instans av GetBookByIdQuery med det ID:t
            var request = new GetBookByIdQuery(bookToReturnId);

            // Act
            var result = await _getBookByIdQueryHandler.Handle(request, CancellationToken.None);  // Skicka queryn istället för ID:t direkt

            // Assert
            Assert.IsNotNull(result, "The returned book should not be null.");
            Assert.AreEqual(bookToReturnId, result.Id, "The returned book's ID should match the requested book's ID.");
        }
    }
}
