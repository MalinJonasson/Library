using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {

        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.NewUser == null ||
              string.IsNullOrWhiteSpace(request.NewUser.UserName) ||
              string.IsNullOrWhiteSpace(request.NewUser.Password))
            {
                throw new ArgumentException("Author name and description cannot be empty or null");
            }

            User userToCreate = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password,
            };

            _userRepository.AddUser(userToCreate);
            return Task.FromResult(userToCreate);
        }
    }
}
