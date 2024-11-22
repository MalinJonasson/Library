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
            Book? bookToDelete = _fakeDatabase.Books.FirstOrDefault(book => book.Id == request.Id);

            if (bookToDelete == null)
            {
                return Task.FromResult<Book>(null!);
            }

            _fakeDatabase.Books.Remove(bookToDelete);

            return Task.FromResult(bookToDelete);
        }
    }
}
