using Auth;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;


namespace Controllers
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
        [AllowAnonymous]
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
                    RefreshToken = token.Refresh_Token,
                    AccessToken = token.Access_Token,
                    IsActive = 0
                };

                UserToken? savedUserToken = userTokenRepository.RetrieveIsInvalid(userToken);
                userToken.IsActive = 1;

                if (savedUserToken is null)
                {
                    if (await userTokenRepository.CreateAsync(userToken) is null)
                    {
                        return Unauthorized("Invalid token!");
                    }
                }
                else
                {
                    savedUserToken.IsActive = 1;
                    if (await userTokenRepository.UpdateAsync(savedUserToken) is null)
                    {
                        return Unauthorized("Invalid token!");
                    }
                }

                return Ok(token);
            }

            return BadRequest("Invalid email or password.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/refresh")]
        public async Task<IActionResult> Refresh([FromForm] Tokens token)
        {
            IEnumerable<Claim>? claims = AuthHelper.TokenDecode(token.Access_Token);
            if (claims is null)
            {
                return Unauthorized("Invalid attempt!");
            }

            UserToken userToken = new()
            {
                UserEmail = claims.First(claim => claim.Type == ClaimTypes.Email).Value,
                RefreshToken = token.Refresh_Token,
                AccessToken = token.Access_Token,
                IsActive = 1
            };

            //User? user = userRepository.Retrieve(userToken.UserEmail);
            User? user = new()
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
            if (savedUserToken.RefreshToken != token.Refresh_Token)
            {
                return Unauthorized("Invalid attempt!");
            }

            Tokens newToken = jwtManager.GenerateRefreshToken(user);

            if (newToken is null)
            {
                return Unauthorized("Invalid attempt!");
            }

            savedUserToken.RefreshToken = newToken.Refresh_Token;
            savedUserToken.AccessToken = newToken.Access_Token;
            await userTokenRepository.UpdateAsync(savedUserToken);

            return Ok(newToken);
        }

        [HttpPost]
        [Authorize]
        [Route("api/logout")]
        public IActionResult Logout([FromForm] Tokens token)
        {
            IEnumerable<Claim>? claims = AuthHelper.TokenDecode(token.Access_Token);

            if (claims is null)
            {
                return BadRequest();
            }

            UserToken userToken = new()
            {
                UserEmail = claims.First(claim => claim.Type == ClaimTypes.Email).Value,
                AccessToken = token.Access_Token,
                RefreshToken = token.Refresh_Token,
                IsActive = 1
            };

            UserToken? savedUserToken = userTokenRepository.Retrieve(userToken);

            if (savedUserToken is not null)
            {
                savedUserToken.IsActive = 0;
                userTokenRepository.UpdateAsync(savedUserToken);
            }

            Response.Headers.Remove("Authorization");

            return Unauthorized();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
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
        [AllowAnonymous]
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

        [Authorize]
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

        [Authorize]
        [HttpPut("api/user/updatePassword/{id}")]
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

                return Ok(flag);
            }

            return BadRequest("Wrong password!");
        }
    }


}
