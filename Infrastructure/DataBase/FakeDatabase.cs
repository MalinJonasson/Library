using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class FakeDatabase
    {
        public List<Book> Books { get { return allBooksFromDB;  } set { allBooksFromDB = value;  } }
        public List<Author> Author { get { return allAuthorsFromDB; } set { allAuthorsFromDB = value; } }

        List<Book> allBooksFromDB = new List<Book>()
        {
            new Book (1, "Book1", "AboutBook1"),
            new Book (2, "Book2", "AboutBook2"),
            new Book (3, "Book3", "AboutBook3"),
            new Book (4, "Book4", "AboutBook4"),
            new Book (5, "Book5", "AboutBook5"),
        };


        List<Author> allAuthorsFromDB = new List<Author>()
        {
            new Author (1, "Author1"),
            new Author (2, "Author2"),
            new Author (3, "Author3"),
            new Author (4, "Author4"),
            new Author (5, "Author5"),
        };

        // My CRUD Methods

        public Book AddNewBook(Book book)
        {
            allBooksFromDB.Add(book);
            return book;
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
