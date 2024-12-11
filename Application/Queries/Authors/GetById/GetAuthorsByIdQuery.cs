using Domain.Models;
using MediatR;

namespace Application.Queries.Authors.GetById
{
    public class GetAuthorsByIdQuery : IRequest<OperationResult<Author>>
    {
        public GetAuthorsByIdQuery(Guid authorId)
        {
            Id = authorId;
        }

        public Guid Id { get; }
    }
}
