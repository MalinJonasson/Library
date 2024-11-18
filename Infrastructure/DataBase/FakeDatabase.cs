using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataBase
{
    public class FakeDatabase
    {
        public List<Book> Books { get { return allBooksFromDB;  } set { allBooksFromDB = value;  } }

        List<Book> allBooksFromDB = new List<Book>()
        {
            new Book (1, "Book1", "AboutBook1"),
            new Book (2, "Book2", "AboutBook2"),
            new Book (3, "Book3", "AboutBook3"),
            new Book (4, "Book4", "AboutBook4"),
            new Book (5, "Book5", "AboutBook5"),
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
    }
}
