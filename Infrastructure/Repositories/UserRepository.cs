using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealDatabase _realDatabase;

        public UserRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }
        public Task<User> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> LogInUser(string username, string password)
        {
            User user = _realDatabase.Users.FirstOrDefault(user => user.UserName == username && user.Password == password);
            if (user is not null)
            {
                return Task.FromResult(user);
            }
            return Task.FromResult(user);
        }
    }
}
