using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Books.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return OperationResult<Book>.Failure("AddBookCommand cannot be null.");
            }

            if (request.NewBook == null)
            {
                return OperationResult<Book>.Failure("NewBook cannot be null.");
            }

            Book bookToCreate = new()
            {
                Id = Guid.NewGuid(),
                Title = request.NewBook.Title,
                Description = request.NewBook.Description,
            };

            _bookRepository.AddBook(bookToCreate);

            return OperationResult<Book>.Success(bookToCreate, "Book added successfully.");
        }
    }
}
