using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiTest.Interfaces;
using WebApiTest.Model;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public UserController(IConfiguration config, IUserRepository userRepository)
        {
            _configuration = config;
            _userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult Post(User data)
        {
            if (data == null || data.Username == null || data.Password == null)
            {
                return BadRequest();
            }
            var user = _userRepository.GetUser(data.Username, data.Password);

            if (user == null)
            {
                return BadRequest("Invalid credentials");
            }
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", user.Username)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signIn

            );
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [HttpPost("Add")]
        public ActionResult AddUser(User User)
        {
            if (User.Username == null || User.Password == null) 
            { return BadRequest("Please enter Username and Password");}
            if (_userRepository.UserExit(User.Username))
            { return BadRequest("User already exist"); }
            var any = _userRepository.CreateUser(User);
            return new JsonResult("Register Successfully");
        }
    }
}
