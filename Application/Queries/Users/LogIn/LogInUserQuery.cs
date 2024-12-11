using Domain.Models;
using MediatR;

namespace Application.Queries.Users.LogIn
{
    public class LogInUserQuery : IRequest<OperationResult<string>>
    {

        public LogInUserQuery(User logInUser)
        {
            LogInUser = logInUser;
        }

        public User LogInUser { get; }
    }
}
