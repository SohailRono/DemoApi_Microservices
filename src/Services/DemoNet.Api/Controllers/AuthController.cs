using Azure.Core;
using BCrypt.Net;
using DemoNet.Api.Interfaces;
using DemoNet.Api.Models.Dtos;
using DemoNet.Api.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace DemoNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration configuration, IUserRepository repository, ILogger<AuthController> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(CustomerInfo), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            var isExists = await _repository.CheckLogin(user.Email);
            if (isExists == null)
            {
                _logger.LogInformation($"This {user.Email} already exists.");
                return BadRequest($"This {user.Email} already exists.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var newUser = await _repository.AddAsync(user);

            _logger.LogInformation($"Customer {user.Id} is successfully created.");
            return CreatedAtRoute(new { id = user.Id }, newUser);
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserDto user)
        {
            var logedUser = await _repository.CheckLogin(user.Email);

            if (logedUser == null)
            {
                return BadRequest("Invalid email");
            }
            if (!BCrypt.Net.BCrypt.Verify(user.Password, logedUser.Password))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }
        private string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
               
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}