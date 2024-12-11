using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand, OperationResult<bool>>
    {
        private readonly IAuthorRepository _authorRepository;


        public DeleteAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

       public async Task<OperationResult<bool>> Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
       {
           if (request == null || request.Id == Guid.Empty)
           {
               return OperationResult<bool>.Failure("Invalid ID or missing required fields.", "Validation error");
           }

           try
           {
               var authorToDelete = await _authorRepository.GetAuthorById(request.Id);

               if (authorToDelete == null)
               {
                   return OperationResult<bool>.Failure($"Author with ID {request.Id} was not found.");
               }

               await _authorRepository.DeleteAuthorById(request.Id);
               return OperationResult<bool>.Success(true, "Author deleted successfully.");
           }
           catch (Exception ex)
           {
               return OperationResult<bool>.Failure($"An error occurred while deleting the author: {ex.Message}");
           }
       }
    }
}