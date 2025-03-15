using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            // code omitted for brevity
            return Ok();
        }
    }
}
//https://youtu.be/6EEltKS8AwA?si=2Z54UCt-m4Jd_GFd&t=531