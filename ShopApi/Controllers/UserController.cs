using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;
        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] Login data)
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
        
        [HttpGet]
        [Authorize]
        [Route("api/users")]
        public async Task<IActionResult> Get()
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

            if(user is null)
            {
                return new NoContentResult();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("api/registration")]
        public async Task<IActionResult> Registration([FromBody] User data)
        {
            return Ok();
        }

        [HttpPut("api/user/update/{id}")]
        //[Route("api/user/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdate data)
        {
            return Ok();
        }
        
        [HttpPut("api/user/updatePassword/{id}")]
        //[Route("api/user/updatePassword/{id}")]
        public async Task<IActionResult> PasswordUpdate(int id, [FromBody] UserUpdatePassword data)
        {
            return Ok();
        }

        [HttpDelete("api/user/{id}")]
        [Authorize]
        //[Route("api/user/{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] string password)
        {
            User? user = await repository.RetrieveAsync(id);

            if(Hash.ValidatePassword(password, user.Password))
            {
                bool flag = await repository.DeleteAsync(id);
                await Logout();
                return Ok(flag);
            }

            return BadRequest("Wrong password!");
        }
    }

   
}
