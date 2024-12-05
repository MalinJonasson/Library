using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries.Authors.GetById
{
    public class GetAuthorsByIdQueryHandler : IRequestHandler<GetAuthorsByIdQuery, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorsByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<Author> Handle(GetAuthorsByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Author wantedAuthor = await _authorRepository.GetAuthorById(request.Id);

            if (wantedAuthor == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.Id} was not found.");
            }

            return wantedAuthor;
        }
    }
}
