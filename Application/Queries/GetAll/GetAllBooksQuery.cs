using Domain.Models;
using MediatR;

namespace Application.Queries.GetAll
{
    public class GetAllBooksQuery : IRequest<List<Book>>
    {

    }
}
