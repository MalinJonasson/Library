using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Authors.DeleteAuthor
{
    public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;


        public DeleteAuthorByIdCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }


        public async Task<Author> Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID or missing required fields");
            }

            Author authorToDelete = await _authorRepository.GetAuthorById(request.Id);

            if (authorToDelete == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.Id} was not found.");
            }

            await _authorRepository.DeleteAuthorById(request.Id);

            return authorToDelete;

        }
    }
}