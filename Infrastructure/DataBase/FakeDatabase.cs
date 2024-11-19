using Domain.Models;
using System.Xml.Linq;

namespace Infrastructure.Database
{
    public class FakeDatabase
    {
        public List<Book> Books { get { return allBooksFromDB; } set { allBooksFromDB = value; } }
        public List<Author> Authors { get { return allAuthorsFromDB; } set { allAuthorsFromDB = value; } }

        List<Book> allBooksFromDB = new List<Book>()
        {
            new Book { Id = Guid.NewGuid(), Title = "Book1", Description = "AboutBook1" },
            new Book { Id = Guid.NewGuid(), Title = "Book2", Description = "AboutBook2" },
            new Book { Id = Guid.NewGuid(), Title = "Book3", Description = "AboutBook3" },
            new Book { Id = Guid.NewGuid(), Title = "Book4", Description = "AboutBook4" },
            new Book { Id = Guid.NewGuid(), Title = "Book5", Description = "AboutBook5" },
        };


        List<Author> allAuthorsFromDB = new List<Author>()
        {
            new Author { Id = Guid.NewGuid(), Name = "Author1" },
            new Author { Id = Guid.NewGuid(), Name = "Author2" },
            new Author { Id = Guid.NewGuid(), Name = "Author3" },
            new Author { Id = Guid.NewGuid(), Name = "Author4" },
            new Author { Id = Guid.NewGuid(), Name = "Author5" },
        };

        // My CRUD Methods

        public Book AddNewBook(Book book)
        {
            allBooksFromDB.Add(book);
            return book;
        }

        public void GetBooksById()
        {

        }

        public void GetAllBooks()
        {

        }

        public void UpdateBooks()
        {

        }

        public void DeleteBook(Book book)
        {
            allBooksFromDB.Remove(book);
        }

        public Author AddNewAuhtor(Author author)
        {
            allAuthorsFromDB.Add(author);
            return author;
        }

        public void DeleteAuthor(Author author)
        {
            allAuthorsFromDB.Remove(author);
        }
    }
}
