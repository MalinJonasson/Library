using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Books.UpdateBook
{
    public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        public UpdateBookByIdCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<Book> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty ||
                string.IsNullOrWhiteSpace(request.UpdatedBook.Title) ||
                string.IsNullOrWhiteSpace(request.UpdatedBook.Description))
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Book bookToUpdate = _fakeDatabase.Books.FirstOrDefault(book => book.Id == request.Id)!;

            if (bookToUpdate == null && string.IsNullOrWhiteSpace(request.UpdatedBook.Title))
            {
                bookToUpdate.Title = request.UpdatedBook.Title;
                bookToUpdate.Description = request.UpdatedBook.Description;
                return Task.FromResult(bookToUpdate);
            }

            throw new ArgumentNullException("Book not found or name cannot be empty");
        }
    }
}
