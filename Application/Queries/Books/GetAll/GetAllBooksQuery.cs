using Domain.Models;
using MediatR;

namespace Application.Queries.Books.GetAll
{
    public class GetAllBooksQuery : IRequest<OperationResult<List<Book>>>
    {

    }
}
