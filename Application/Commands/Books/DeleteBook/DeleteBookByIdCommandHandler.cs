using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Books.DeleteBook
{
    public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        public DeleteBookByIdCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<Book> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Book? bookToDelete = _fakeDatabase.Books.FirstOrDefault(book => book.Id == request.Id);

            _fakeDatabase.Books.Remove(bookToDelete);

            return Task.FromResult(bookToDelete);
        }
    }
}
