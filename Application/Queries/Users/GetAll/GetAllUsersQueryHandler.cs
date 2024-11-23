using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAllUsersQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> allUsersFromFakeDatabase = _fakeDatabase.Users;

            if (allUsersFromFakeDatabase == null || !allUsersFromFakeDatabase.Any())
            {
                throw new ArgumentException("Userlist is empty or null");
            }

            return Task.FromResult(allUsersFromFakeDatabase);
        }
    }
}
