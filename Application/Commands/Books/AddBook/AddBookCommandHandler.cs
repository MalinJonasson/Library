using MediatR;
using Domain.Models;
using Infrastructure.Database;

namespace Application.Commands.Books.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        public AddBookCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.NewBook == null ||
                string.IsNullOrWhiteSpace(request.NewBook.Title) ||
                string.IsNullOrWhiteSpace(request.NewBook.Description))
            {
                throw new ArgumentException("Author name and description cannot be empty or null");
            }

            var author = _fakeDatabase.Authors.FirstOrDefault(a => a.Id == request.NewBook.AuthorId);
            if (author == null)
            {
                throw new ArgumentException("Invalid AuthorId, no author found with the provided ID");
            }

            Book bookToCreate = new()
            {
                Id = Guid.NewGuid(),
                Title = request.NewBook.Title,
                Description = request.NewBook.Description,
                AuthorId = request.NewBook.AuthorId,
                Author = author
            };

            _fakeDatabase.Books.Add(bookToCreate);

            return Task.FromResult(bookToCreate);
        }
    }
}
