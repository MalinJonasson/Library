using Domain.Models;

namespace Infrastructure.Database
{
    public class FakeDatabase
    {
        public List<Book> Books
        {
            get { return allBooksFromDB; }
            set { allBooksFromDB = value; }
        }

        List<Book> allBooksFromDB = new List<Book>()
        {
            new Book { Id = Guid.NewGuid(), Title = "Book1", Description = "AboutBook1"},
            new Book { Id = Guid.NewGuid(), Title = "Book2", Description = "AboutBook2"},
            new Book { Id = Guid.NewGuid(), Title = "Book3", Description = "AboutBook3"},
            new Book { Id = Guid.NewGuid(), Title = "Book4", Description = "AboutBook4"},
            new Book { Id = Guid.NewGuid(), Title = "Book5", Description = "AboutBook5"},
        };

        public List<Author> Authors
        {
            get { return allAuthorsFromDB; }
            set { allAuthorsFromDB = value; }
        }

        List<Author> allAuthorsFromDB = new List<Author>()
        {
            new Author { Id = Guid.NewGuid(), Name = "Author1" },
            new Author { Id = Guid.NewGuid(), Name = "Author2" },
            new Author { Id = Guid.NewGuid(), Name = "Author3" },
            new Author { Id = Guid.NewGuid(), Name = "Author4" },
            new Author { Id = Guid.NewGuid(), Name = "Author5" },
        };

        public List<User> Users
        {
            get { return allUsersFromDB; }
            set { allUsersFromDB = value; }
        }

        List<User> allUsersFromDB = new List<User>()
        {
            new User { Id = Guid.NewGuid(), UserName = "User1" },
            new User { Id = Guid.NewGuid(), UserName = "User2" },
            new User { Id = Guid.NewGuid(), UserName = "User3" },
        };
    }
}
