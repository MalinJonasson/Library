using Domain.Models;
using MediatR;

namespace Application.Queries.Books.GetById
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public GetBookByIdQuery(Guid bookId)
        {
            Id = bookId;
        }

        public Guid Id { get; }
    }
}
