using Application.Interfaces.RepositoryInterfaces;
using Application.Queries.Users.LogIn.Helpers;
using MediatR;

namespace Application.Queries.Users.LogIn
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public LogInUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }
        public async Task<string> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.LogInUser.UserName) || string.IsNullOrWhiteSpace(request.LogInUser.Password))
            {
                throw new ArgumentException("Username and password cannot be empty");
            }

            var user = await _userRepository.LogInUser(request.LogInUser.UserName, request.LogInUser.Password);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user);

            return token;
        }
    }
}
