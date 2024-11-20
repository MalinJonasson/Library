using Application.Queries.GetAll;
using Application.Queries.GetAllById;
using Domain.Models;
using Infrastructure.Database;

namespace Test.QueryTest.AuthorTest
{
    public class AuthorQueryUnitTest
    {
        private FakeDatabase _fakeDatabase;
        private GetAllAuthorsQueryHandler _getAllAuthorsQueryHandler;
        private GetAuthorsByIdQueryHandler _getAuthorsByIdQueryHandler;

        [SetUp]
        public void SetUp()
        {
            // Initialize FakeDatabase and handler before each test
            _fakeDatabase = new FakeDatabase();
            _getAllAuthorsQueryHandler = new GetAllAuthorsQueryHandler(_fakeDatabase);
            _getAuthorsByIdQueryHandler = new GetAuthorsByIdQueryHandler(_fakeDatabase);
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
