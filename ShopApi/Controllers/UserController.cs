using Helpers;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace ShopApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly IConfiguration configuration;

        public UserController(IUserRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] Login data)
        {
            User? user = repository.Retrieve(data.Email);



            if (user is not null && Auth.ValidatePassword(data.Password, user.Password))
            {
                List<Claim> claims = new()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType
                    );

                SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

                DateTime now = DateTime.UtcNow;
                
                JwtSecurityToken jwt = new(
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        notBefore: now,
                        claims: claimsIdentity.Claims,
                        expires: now.AddMinutes(Convert.ToInt32(configuration["Jwt:Limit"])),
                        signingCredentials: credentials
                        );
                string tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(new
                {
                    token = tokenString,
                    id = user.UserId,
                    email = user.Email,
                    Role = user.Role.Name
                });
            }

            return BadRequest("Invalid email or password.");
        }

        [HttpGet]
        [Authorize]
        [Route("api/logout")]
        public IActionResult Logout()
        {
            return Unauthorized();
        }

        [HttpGet]
        [Authorize()]
        [Route("api/users")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<User> users = await repository.RetrieveAllAsync();
            return Ok(users);
        }

        [HttpGet("api/user/{id}")]
        [Authorize]
        //[Route("api/user/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User? user = await repository.RetrieveAsync(id);

            if (user is null)
            {
                return new NoContentResult();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("api/registration")]
        public async Task<IActionResult> Registration([FromBody] User data)
        {
            data.CreateAt = DateTime.Now;

            User? user = await repository.CreateAsync(data);

            if (user is null)
            {
                return BadRequest();
            }

            Login login = new();

            login.Email = data.Email;
            login.Password = data.Password;

            return Login(login);
        }

        [HttpPut("api/user/update/{id}")]
        //[Route("api/user/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdate data)
        {
            User newUser = new()
            {
                UserId = id,
                Name = data.Name,
                Phone = data.Phone,
                Address = data.Address,
                CityId = data.CityId,
                PostCod = data.PostCod,
            };
            User? user = await repository.UpdateAsync(id, newUser);
            if (user is null)
            {
                return BadRequest("Failed to edit!");
            }

            return Ok();
        }

        [HttpPut("api/user/updatePassword/{id}")]
        //[Route("api/user/updatePassword/{id}")]
        public async Task<IActionResult> PasswordUpdate(int id, [FromBody] UserUpdatePassword data)
        {
            User newUser = new()
            {
                UserId = id,
                Password = data.Password,
            };
            User? user = await repository.UpdateAsync(id, newUser);

            if (user is null)
            {
                return BadRequest("Failed to edit!");
            }

            return Ok();
        }

        [HttpDelete("api/user/{id}")]
        [Authorize]
        //[Route("api/user/{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] string password)
        {
            User? user = await repository.RetrieveAsync(id);

            if (user is not null && Auth.ValidatePassword(password, user.Password))
            {
                bool flag = await repository.DeleteAsync(id);
                Logout();
                return Ok(flag);
            }

            return BadRequest("Wrong password!");
        }
    }


}
