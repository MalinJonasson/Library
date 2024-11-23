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
            Book bookToUpdate = _fakeDatabase.Books.FirstOrDefault(book => book.Id == request.Id);

            bookToUpdate.Title = request.UpdatedBook.Title;
            bookToUpdate.Description = request.UpdatedBook.Description;

            if (request.UpdatedBook.AuthorId != Guid.Empty)
            {
                var authorToAddToBook = _fakeDatabase.Authors.FirstOrDefault(a => a.Id == request.UpdatedBook.AuthorId);

                if (authorToAddToBook == null)
                {
                    throw new ArgumentException("Invalid AuthorId, no author found with the provided ID");
                }

                bookToUpdate.AuthorId = authorToAddToBook.Id;
                bookToUpdate.Author = authorToAddToBook;
            }

            return Task.FromResult(bookToUpdate);
        }

    }
}
