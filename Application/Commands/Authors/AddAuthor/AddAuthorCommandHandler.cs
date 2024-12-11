using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

       public async Task<OperationResult<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.NewAuthor == null || string.IsNullOrWhiteSpace(request.NewAuthor.Name))
            {
                return OperationResult<Author>.Failure("Author name cannot be empty or null.", "Validation error");
            }

            try
            {
                var authorToCreate = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = request.NewAuthor.Name
                };

                await _authorRepository.AddAuthor(authorToCreate);
                return OperationResult<Author>.Success(authorToCreate, "Author added successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure($"An error occurred while adding the author: {ex.Message}");
            }
        }
    }
}
