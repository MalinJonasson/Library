using Application.Queries.Authors.GetAll;
using Application.Queries.Authors.GetById;
using Application.Queries.Books.GetById;
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
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count);
            Assert.Contains(author1, result);
            Assert.Contains(author2, result);

        }

        [Test]
        public async Task Handle_ShouldGetAuthorsByIdFromFakeDatabaseAndReturnAuthor()
        {
            // Arrange
            var authorToReturnId = new Guid("fa7c2886-a981-43dc-9acb-666dcf9025e3");
            var authorToReturn = new Author { Id = authorToReturnId, Name = "Test Author" };
            _fakeDatabase.Authors.Add(authorToReturn);

            var request = new GetAuthorsByIdQuery(authorToReturnId);

            // Act
            var result = await _getAuthorsByIdQueryHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(authorToReturnId, result.Id);
        }

    }
}
