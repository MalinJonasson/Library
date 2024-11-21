using Domain.Models;
using MediatR;

namespace Application.Commands.Users
{
    public class AddUserCommand : IRequest<User>
    {

        public AddUserCommand(User newUser)
        {
            NewUser = newUser;
        }

        public User NewUser { get; }
    }
}
