using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        public IRoleRepository repository;

        public RoleController(IRoleRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Role>> Get()
        {
            return await repository.RetrieveAllAsync();
        }
    }
}
