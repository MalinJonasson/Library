using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookByIdCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Book> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty ||
                string.IsNullOrWhiteSpace(request.UpdatedBook.Title) ||
                string.IsNullOrWhiteSpace(request.UpdatedBook.Description))
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }


            Book bookToUpdate = await _bookRepository.GetBookById(request.Id);

            if (bookToUpdate == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.Id} was not found.");
            }

            bookToUpdate.Title = request.UpdatedBook.Title;
            bookToUpdate.Description = request.UpdatedBook.Description;

            Book updatedBook = await _bookRepository.UpdateBookById(request.Id, bookToUpdate);

            return updatedBook;

        }

    }
}
