using Domain.Models;
using MediatR;

namespace Application.Queries.GetAll
{
    public class GetAllAuthorsQuery : IRequest<List<Author>>
    {
    }
}
