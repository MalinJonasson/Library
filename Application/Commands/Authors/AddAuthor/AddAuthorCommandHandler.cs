using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.NewAuthor == null || string.IsNullOrWhiteSpace(request.NewAuthor.Name))
            {
                throw new ArgumentException("Author name cannot be empty or null");
            }

            Author authorToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewAuthor.Name
            };

            _authorRepository.AddAuthor(authorToCreate);

            return Task.FromResult(authorToCreate);
        }

    }
}
