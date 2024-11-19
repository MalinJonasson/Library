using Application.Commands.AddBook;
using Application.Commands.DeleteBook;
using Application.Commands.UpdateBook;
using Application.Queries.GetAll;
using Application.Queries.GetAllById;
using Domain.Models;
using Infrastructure.Database;

namespace Test.CommandTest.BookTest
{
    public class BookCommandUnitTest
    {

        private FakeDatabase _fakeDatabase;
        private AddBookCommandHandler _addBookCommandHandler;
        private DeleteBookByIdCommandHandler _deleteBookByIdCommandHandler;
        private UpdateBookByIdCommandHandler _updateBookByIdCommandHandler;
        private GetAllBooksQueryHandler _getAllBooksQueryHandler;
        private GetBookByIdQueryHandler _getBookByIdQueryHandler;

        [SetUp]
        public void SetUp()
        {
            // Initialize FakeDatabase and handler before each test
            _fakeDatabase = new FakeDatabase();
            _addBookCommandHandler = new AddBookCommandHandler(_fakeDatabase);
            _deleteBookByIdCommandHandler = new DeleteBookByIdCommandHandler(_fakeDatabase);
            _updateBookByIdCommandHandler = new UpdateBookByIdCommandHandler(_fakeDatabase);
            _getAllBooksQueryHandler = new GetAllBooksQueryHandler(_fakeDatabase);
            _getBookByIdQueryHandler = new GetBookByIdQueryHandler(_fakeDatabase);
        }

        [Test]
        public async Task Handle_ShouldAddBookToFakeDatabaseAndReturnBook()
        {
            // Arrange
            var newBook = new Book { Title = "Test Book" };
            var request = new AddBookCommand(newBook);

            // Act
            var result = await _addBookCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "The returned book should not be null.");
            Assert.AreEqual(newBook.Title, result.Title, "The book's tilte should match the input.");
            Assert.IsTrue(_fakeDatabase.Books.Contains(result), "The book should be added to the database.");
        }

        [Test]
        public async Task Handle_ShouldDeleteBookFromFakeDatabaseAndReturnBook()
        {
            // Arrange
            var bookToDeleteId = new Guid("fa7c2886-a981-43dc-9acb-666dcf9025e3");

            // Lägg till en bok i _fakeDatabase med det ID:t
            var bookToDelete = new Book { Id = bookToDeleteId, Title = "Test Book" };
            _fakeDatabase.Books.Add(bookToDelete); // Lägg till i databasen

            // Skapa en instans av GetAuthorsByIdQuery med det ID:t
            var request = new DeleteBookByIdCommand(bookToDeleteId);
            // Act
            var result = await _deleteBookByIdCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result, "The returned book should not be null."); // boken som togs bort ska returneras
            Assert.AreEqual(bookToDeleteId, result.Id, "The returned book's ID should match the deleted book's ID.");
            Assert.IsFalse(_fakeDatabase.Books.Any(b => b.Id == bookToDeleteId), "The book should be removed from the database.");

        }


        [Test]
        public async Task Handle_ShouldUpdateOldBooksNameByIdFromFakeDatabaseAndReturnNewBooksName()
        {
            // Arrange
            var bookToUpdateId = Guid.NewGuid(); // Generera ett nytt GUID för den nya boken
            var existingbook = new Book { Id = bookToUpdateId, Title = "Old book name" };

            _fakeDatabase.Books.Add(existingbook); // Lägg till den skapade boken i databasen

            var updatedBook = new Book { Title = "Updated book name" }; // boken ska uppdatera sitt namn
            var request = new UpdateBookByIdCommand(updatedBook, bookToUpdateId); // Skapa kommandot för uppdatering

            // Act
            var result = await _updateBookByIdCommandHandler.Handle(request, CancellationToken.None); // Kör hanteraren

            // Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual(bookToUpdateId, result.Id, "The books's ID should match the updated ID.");
            Assert.AreEqual(updatedBook.Title, result.Title, "The books's name should be updated.");

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
