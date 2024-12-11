using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries.Authors.GetById
{
    public class GetAuthorsByIdQueryHandler : IRequestHandler<GetAuthorsByIdQuery, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorsByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
       public async Task<OperationResult<Author>> Handle(GetAuthorsByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return OperationResult<Author>.Failure("Invalid ID or missing required fields.", "Validation error");
            }

            try
            {
                var wantedAuthor = await _authorRepository.GetAuthorById(request.Id);

                if (wantedAuthor == null)
                {
                    return OperationResult<Author>.Failure($"Author with ID {request.Id} was not found.");
                }

                return OperationResult<Author>.Success(wantedAuthor, "Author retrieved successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure($"An error occurred while retrieving the author: {ex.Message}");
            }
        }
    }
}
