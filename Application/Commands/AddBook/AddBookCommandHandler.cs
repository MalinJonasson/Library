using MediatR;
using Domain.Models;
using Infrastructure.Database;

namespace Application.Commands.AddBook
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
            Book bookToCreate = new()
            {
                Id = Guid.NewGuid(),
                Title = request.NewBook.Title,
                Description = request.NewBook.Description
            };

            _fakeDatabase.Books.Add(bookToCreate);

            return Task.FromResult(bookToCreate);
        }
    }
}
