using Domain.Models;
using Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class BookMethods
    {
        private readonly FakeDatabase _fakeDatabase;

        public BookMethods(){}
        public BookMethods(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        //Vallidering/felhantering
        public Book AddNewBook()
        {
            //How to process data in application

            Book newBookToAdd = new Book(1, "Book1", "AboutBook1");

            //Seperations of consern, returnerar vad infrastrukturen gör
            return _fakeDatabase.AddNewBook(newBookToAdd);
        }

        public Book DeleteBook()
        {
            throw new NotImplementedException();
        }

        public Book GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book UpdateABook()
        {
            throw new NotImplementedException();
        }
    }
}
