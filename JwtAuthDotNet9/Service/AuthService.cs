using JwtAuthDotNet9.Data;
using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;

namespace JwtAuthDotNet9.Service
{
    public class AuthService(UserDbContext context, IConfiguration configuration) : IAuthService
    {
        public Task<string?> LoginAsync(UserDto request)
        {
            throw new NotImplementedException();
        }
        public Task<User?> RegisterAsync(UserDto request)
        {
            throw new NotImplementedException();
        }
    }
}

//https://youtu.be/6EEltKS8AwA?si=KWYtVYLmjIeOuWpq&t=2412
