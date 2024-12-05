using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Books.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.NewBook == null ||
                string.IsNullOrWhiteSpace(request.NewBook.Title) ||
                string.IsNullOrWhiteSpace(request.NewBook.Description))
            {
                throw new ArgumentException("Author name and description cannot be empty or null");
            }

            Book bookToCreate = new()
            {
                Id = Guid.NewGuid(),
                Title = request.NewBook.Title,
                Description = request.NewBook.Description,
            };

            _bookRepository.AddBook(bookToCreate);

            return Task.FromResult(bookToCreate);
        }
    }
}
