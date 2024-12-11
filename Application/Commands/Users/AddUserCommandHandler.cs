using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Commands.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, OperationResult<User>>
    {

        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.NewUser == null ||
                string.IsNullOrWhiteSpace(request.NewUser.UserName) ||
                string.IsNullOrWhiteSpace(request.NewUser.Password))
            {
                return OperationResult<User>.Failure("User name and password cannot be empty or null.", "Validation error");
            }

            try
            {
                User userToCreate = new()
                {
                    Id = Guid.NewGuid(),
                    UserName = request.NewUser.UserName,
                    Password = request.NewUser.Password,
                };

                await _userRepository.AddUser(userToCreate);
                return OperationResult<User>.Success(userToCreate, "User created successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<User>.Failure($"An error occurred while creating the user: {ex.Message}");
            }
        }
    }
}
