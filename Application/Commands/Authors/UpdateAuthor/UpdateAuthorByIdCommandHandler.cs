using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommandHandler : IRequestHandler<UpdateAuthorByIdCommand, OperationResult<Author>>
    {

        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

       public async Task<OperationResult<Author>> Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
        { 
            if (request == null || request.Id == Guid.Empty || string.IsNullOrWhiteSpace(request.UpdatedAuthor.Name))
            {
                return OperationResult<Author>.Failure("Invalid ID or missing required fields.", "Validation error");
            }

            try
            {
                var existingAuthor = await _authorRepository.GetAuthorById(request.Id);

                if (existingAuthor == null)
                {
                    return OperationResult<Author>.Failure($"Author with ID {request.Id} was not found.");
                }

                existingAuthor.Name = request.UpdatedAuthor.Name;
                var updatedAuthor = await _authorRepository.UpdateAuthorById(request.Id, existingAuthor);
                return OperationResult<Author>.Success(updatedAuthor, "Author updated successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure($"An error occurred while updating the author: {ex.Message}");
            }
        }
    }
}
