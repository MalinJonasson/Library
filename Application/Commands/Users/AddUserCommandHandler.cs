using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly FakeDatabase _fakeDatabase;

        public AddUserCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User userToCreate = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password,
            };

            _fakeDatabase.Users.Add(userToCreate);

            return Task.FromResult(userToCreate);
        }
    }
}
