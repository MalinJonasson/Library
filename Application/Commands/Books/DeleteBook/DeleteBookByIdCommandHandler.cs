using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Book> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Book bookToDelete = await _bookRepository.GetBookById(request.Id);

            if (bookToDelete == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.Id} was not found.");
            }

            await _bookRepository.DeleteBookById(request.Id);

            return bookToDelete;
        }
    }
}
