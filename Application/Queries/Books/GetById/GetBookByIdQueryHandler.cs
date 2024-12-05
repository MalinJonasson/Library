using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Queries.Books.GetById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {

        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Book wantedBook = await _bookRepository.GetBookById(request.Id);

            if (wantedBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.Id} was not found.");
            }

            return wantedBook;
        }
    }
}
