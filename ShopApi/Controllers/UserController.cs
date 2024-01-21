using Auth;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace ShopApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly IJWTManagerRepository jwtManager;
        private readonly IUserTokenRepository userTokenRepository;

        public UserController(IUserRepository userRepository,
            IConfiguration configuration,
            IJWTManagerRepository jwtManager,
            IUserTokenRepository userTokenRepository
            )
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.jwtManager = jwtManager;
            this.userTokenRepository = userTokenRepository;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login()
        {
            //User? user = repository.Retrieve(data.Email);
            User user = new()
            {
                UserId = 5,
                Email = "admin@mail.ru",
                Role = new()
                {
                    Name = "admin",
                }
            };

            if (true)
            {
                
                Tokens token = jwtManager.GenerateToken(user);

                UserToken userToken = new()
                {
                    UserEmail = user.Email,
                    Token = token.Refresh_Token
                };

                if (await userTokenRepository.CreateAsync(userToken) is null)
                {
                    return Unauthorized("Invalid token!");
                }


                return Ok(token);
            }

            return BadRequest("Invalid email or password.");
        }

        [HttpPost]
        [Route("api/refresh")]
        public async Task<IActionResult> Refresh([FromForm] Tokens token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken? securityToken = tokenHandler.ReadToken(token.Access_Token);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if(jwtSecurityToken is null)
            {
                return Unauthorized("Invalid attempt!");
            }

            UserToken userToken = new()
            {
                UserEmail = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value,
                Token = token.Refresh_Token
            };

            //User? user = userRepository.Retrieve(userToken.UserEmail);
            User user = new()
            {
                UserId = 5,
                Email = "admin@mail.ru",
                Role = new()
                {
                    Name = "admin",
                }
            };

            UserToken? savedUserToken = userTokenRepository.Retrieve(userToken);
            if (savedUserToken is null || user is null)
            {
                return Unauthorized("Invalid attempt!");
            }
            if (savedUserToken.Token != token.Refresh_Token)
            {
                return Unauthorized("Invalid attempt!");
            }

            Tokens newToken = jwtManager.GenerateRefreshToken(user);

            if (newToken == null)
            {
                return Unauthorized("Invalid attempt!");
            }

            UserToken? userTokenDelete = userTokenRepository.Retrieve(userToken);

            if(userTokenDelete is not null)
            {
                await userTokenRepository.DeleteAsync(userTokenDelete);
            }
            
            userToken.Token = newToken.Refresh_Token;
            await userTokenRepository.CreateAsync(userToken);
            
            return Ok(newToken);
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
            IEnumerable<User> users = await userRepository.RetrieveAllAsync();
            return Ok(users);
        }

        [HttpGet("api/user/{id}")]
        [Authorize]
        //[Route("api/user/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User? user = await userRepository.RetrieveAsync(id);

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

            User? user = await userRepository.CreateAsync(data);

            if (user is null)
            {
                return BadRequest();
            }

            UserLogin login = new();

            login.Email = data.Email;
            login.Password = data.Password;

            return await Login();
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
            User? user = await userRepository.UpdateAsync(id, newUser);
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
            User? user = await userRepository.UpdateAsync(id, newUser);

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
            User? user = await userRepository.RetrieveAsync(id);

            if (user is not null && AuthHelper.ValidatePassword(password, user.Password))
            {
                bool flag = await userRepository.DeleteAsync(id);
                Logout();
                return Ok(flag);
            }

            return BadRequest("Wrong password!");
        }
    }


}
