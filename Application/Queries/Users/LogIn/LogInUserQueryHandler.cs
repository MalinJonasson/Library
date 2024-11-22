using Application.Queries.Users.LogIn.Helpers;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Users.LogIn
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, string>
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly TokenHelper _tokenHelper;

        public LogInUserQueryHandler(FakeDatabase fakeDatabase, TokenHelper tokenHelper)
        {
            _fakeDatabase = fakeDatabase;
            _tokenHelper = tokenHelper;
        }
        public Task<string> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
            var user = _fakeDatabase.Users.FirstOrDefault(user => user.UserName == request.LogInUser.UserName && user.Password == request.LogInUser.Password);
            if (user == null)

            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user);
            return Task.FromResult(token);
        }
    }
}
