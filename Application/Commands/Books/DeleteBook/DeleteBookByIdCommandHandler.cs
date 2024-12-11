using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand, OperationResult<bool>>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        public async Task<OperationResult<bool>> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return OperationResult<bool>.Failure("Invalid ID or missing required fields.", "Validation error");
            }

            var bookToDelete = await _bookRepository.GetBookById(request.Id);
            if (bookToDelete == null)
            {
                return OperationResult<bool>.Failure($"Book with ID {request.Id} was not found.", "Not Found");
            }

            try
            {
                await _bookRepository.DeleteBookById(request.Id);
                return OperationResult<bool>.Success(true, "Book deleted successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure($"An error occurred while deleting the book: {ex.Message}");
            }
        }
    }
}
