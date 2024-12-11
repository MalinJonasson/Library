using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries.Books.GetAll
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, OperationResult<List<Book>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

       public async Task<OperationResult<List<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
       {
            try
            {
                var allBooks = await _bookRepository.GetAllBooks();

                if (allBooks == null || !allBooks.Any())
                {
                    return OperationResult<List<Book>>.Failure("No books found in the database.", "Not Found");
                }

                return OperationResult<List<Book>>.Success(allBooks, "Books retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Hanterar oväntade fel och returnerar ett felresultat
                return OperationResult<List<Book>>.Failure($"An error occurred while retrieving books: {ex.Message}");
            }
        }
    }
}
