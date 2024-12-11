using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealDatabase _realDatabase;

        public UserRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }
        public async Task<User> AddUser(User user)
        {
            _realDatabase.Users.Add(user);
            _realDatabase.SaveChanges();
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await Task.FromResult(_realDatabase.Users.ToList());
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
