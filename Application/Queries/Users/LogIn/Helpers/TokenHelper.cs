using Microsoft.Extensions.Configuration;

namespace Application.Queries.Users.LogIn.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration _configuration;

        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
