using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.UpdateBook
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
            Book bookToUpdate = _fakeDatabase.Books.FirstOrDefault(book => book.Id == request.Id)!;

            bookToUpdate.Title = request.UpdatedBook.Title;

            return Task.FromResult(bookToUpdate);
        }
    }
}
