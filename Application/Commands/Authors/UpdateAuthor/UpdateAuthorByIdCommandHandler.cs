using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.UpdateAuthor
{
    public class UpdateAuthorByIdCommandHandler : IRequestHandler<UpdateAuthorByIdCommand, Author>
    {

        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty || string.IsNullOrWhiteSpace(request.UpdatedAuthor.Name))
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Author existingAuthor = await _authorRepository.GetAuthorById(request.Id);

            if (existingAuthor == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.Id} was not found.");
            }

            existingAuthor.Name = request.UpdatedAuthor.Name;

            Author updatedAuthor = await _authorRepository.UpdateAuthorById(request.Id, existingAuthor);

            return updatedAuthor;

        }
    }
}
