using Application;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        //CRUD GET UPDATE/PUT/PATCH POST DELETE

        [HttpPost]
        public Book AddBook()
        {
            BookMethods applicationLayerBookMethods = new BookMethods();
            return applicationLayerBookMethods.AddNewBook();
        }

        [HttpGet]
        public Book GetAllBooks()
        {
            BookMethods applicationLayerBookMethods = new BookMethods();
            return applicationLayerBookMethods.GetAllBooks();
        }

        [HttpPut]
        public Book UpdateABook()
        {
            BookMethods applicationLayerBookMethods = new BookMethods();
            return applicationLayerBookMethods.UpdateABook();
        }

        [HttpDelete]
        public Book DeleteSBook()
        {
            BookMethods applicationLayerBookMethods = new BookMethods();
            return applicationLayerBookMethods.DeleteBook();
        }
    }
}
