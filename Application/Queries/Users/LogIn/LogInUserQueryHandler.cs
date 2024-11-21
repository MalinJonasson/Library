using Infrastructure.Database;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace Application.Queries.Users.LogIn
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, string>
    {
        private readonly FakeDatabase _fakeDatabase;

        public LogInUserQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<string> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
           var user = _fakeDatabase.Users.FirstOrDefault(user => user.UserName == request.LogInUser.UserName && user.Password == request.LogInUser.Password);
            if (user == null)

            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string token = "Token To Return";
            return Task.FromResult(token);
        }
    }
}
