using Application.Commands.AddAuthor;
using Application.Commands.DeleteAuthor;
using Application.Commands.UpdateAuthor;
using Application.Queries.GetAll;
using Application.Queries.GetAllById;
using Domain.Models;
using MediatR;

namespace Application
{
    public class AuthorMethods
    {
        private readonly IMediator _mediator;

        public AuthorMethods() { }

        public AuthorMethods(IMediator mediator)
        {
            _mediator = mediator;
        }
        //Vallidering/felhantering
        public async void AddNewAuthor(Author author)
        {
            await _mediator.Send(new AddAuthorCommand(author));
        }

        public async void DeleteAuthor(Guid authorToDeleteId)
        {
            await _mediator.Send(new DeleteAuthorByIdCommand(authorToDeleteId));
        }

        public async void UpdateABook(Author updatedAuthor, Guid updatedAuthorId)
        {
            await _mediator.Send(new UpdateAuthorByIdCommand(updatedAuthor, updatedAuthorId));
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return (await _mediator.Send(new GetAllAuthorsQuery()));
        }

        public async void GetAllAuthorsById(Guid authorId)
        {
            await _mediator.Send(new GetAuthorsByIdQuery(authorId));
        }
    }
}
