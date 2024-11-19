using Domain.Models;
using MediatR;

namespace Application.Queries.GetAllById
{
    public class GetAuthorsByIdQuery : IRequest<Author>
    {
        public GetAuthorsByIdQuery(Guid authorId)
        {
            Id = authorId;
        }

        public Guid Id { get; }
    }
}
