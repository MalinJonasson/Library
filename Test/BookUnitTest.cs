using Application;
using Domain;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void When_AddNewBook_Is_Called_Then_Book_Is_Added_To_List()
        {
            // Arrange
            Book bookToTest = new Book(1, "Book1", "AboutBook1");

            // Act
            Book bookCreated = BookMethods.AddNewBook();

            // Assert
            Assert.That(bookCreated, Is.Not.Null);
            Assert.That(bookCreated.Description, Is.EqualTo(bookToTest.Description));

        }
    }
}