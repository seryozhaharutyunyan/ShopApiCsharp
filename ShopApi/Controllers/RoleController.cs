using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        public readonly IRoleRepository repository;

        public RoleController(IRoleRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Role> roles = await repository.RetrieveAllAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Role? role = await repository.RetrieveAsync(id);

            if (role is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] Role data)
        {
            Role? role = await repository.CreateAsync(data);
            if (role is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Role data)
        {
            Role? role = await repository.UpdateAsync(id, data);

            if(role is null)
            {
                return new NoContentResult();
            }

            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool? flag = await repository.DeleteAsync(id);
            if (flag == false || flag is null)
            {
                return new NoContentResult();
            }

            return Ok();
        }
    }
}
