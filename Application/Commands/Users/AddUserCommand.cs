using Domain.Models;
using MediatR;

namespace Application.Commands.Users
{
    public class AddUserCommand : IRequest<OperationResult<User>>
    {

        public AddUserCommand(User newUser)
        {
            NewUser = newUser;
        }

        public User NewUser { get; }
    }
}
