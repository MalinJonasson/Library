using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult<List<Author>>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<OperationResult<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allAuthors = await _authorRepository.GetAllAuthors();
                if (allAuthors == null || !allAuthors.Any())
                {
                    return OperationResult<List<Author>>.Failure("Author list is empty or null.", "No authors found.");
                }

                return OperationResult<List<Author>>.Success(allAuthors, "Authors retrieved successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<List<Author>>.Failure($"An error occurred while retrieving authors: {ex.Message}");
            }
        }
    }
}
