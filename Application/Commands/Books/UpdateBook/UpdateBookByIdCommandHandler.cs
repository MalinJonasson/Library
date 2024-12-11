using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<OperationResult<Book>> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty ||
               string.IsNullOrWhiteSpace(request.UpdatedBook.Title) ||
               string.IsNullOrWhiteSpace(request.UpdatedBook.Description))
            {
                return OperationResult<Book>.Failure("Invalid ID or missing required fields.", "Validation error");
            }

            var bookToUpdate = await _bookRepository.GetBookById(request.Id);
            if (bookToUpdate == null)
            {
                return OperationResult<Book>.Failure($"Book with ID {request.Id} was not found.", "Not Found");
            }

            bookToUpdate.Title = request.UpdatedBook.Title;
            bookToUpdate.Description = request.UpdatedBook.Description;

            try
            {
                var updatedBook = await _bookRepository.UpdateBookById(request.Id, bookToUpdate);
                return OperationResult<Book>.Success(updatedBook, "Book updated successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure($"An error occurred while updating the book: {ex.Message}");
            }
        }
    }
}
