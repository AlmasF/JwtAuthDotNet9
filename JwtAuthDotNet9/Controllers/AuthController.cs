using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration configuration) : ControllerBase
    {
        public static User user = new();
        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }


        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User not found");
            }

            if (
                new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) 
                == PasswordVerificationResult.Failed
            )
            {
                return BadRequest("Wrong password");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            // ===== new version ==================== probably
            //var claims = new Dictionary<string, object>
            //{
            //    [ClaimTypes.Name] = user.Username,
            //};


            //var key = new SymmetricSecurityKey(
            //    Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Issuer = configuration.GetValue<string>("AppSettings:Issuer"),
            //    Audience = configuration.GetValue<string>("AppSettings:Audience"),
            //    Claims = claims,
            //    IssuedAt = null,
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    SigningCredentials = creds
            //};

            //var handler = new JsonWebTokenHandler();
            //handler.SetDefaultTimesOnTokenCreation = false;
            //var tokenString = handler.CreateToken(tokenDescriptor);

            //return tokenString;

            // ===== old version ====================

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
//https://youtu.be/6EEltKS8AwA?si=2Z54UCt-m4Jd_GFd&t=531