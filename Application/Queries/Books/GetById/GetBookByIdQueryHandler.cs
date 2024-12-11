using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries.Books.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, OperationResult<Book>>
    {

        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                return OperationResult<Book>.Failure("Invalid ID or missing required fields.", "Validation error");
            }

            try
            {
                var wantedBook = await _bookRepository.GetBookById(request.Id);

                if (wantedBook == null)
                {
                    return OperationResult<Book>.Failure($"Book with ID {request.Id} was not found.");
                }

                return OperationResult<Book>.Success(wantedBook, "Book retrieved successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<Book>.Failure($"An error occurred while retrieving the book: {ex.Message}");
            }
        }
    }
}
