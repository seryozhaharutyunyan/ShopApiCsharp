using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository repository;

        public CategoryController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]   
        public async Task<IActionResult> Get()
        {
            IEnumerable<Category> categories = await repository.RetrieveAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Category? category = await repository.RetrieveAsync(id);

            if(category is null)
            {
                return new NotFoundResult();
            }
            
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] Category data)
        {
            Category? category = await repository.CreateAsync(data);

            if(category is null)
            {
                return new NotFoundResult();
            }

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category data)
        {
            Category? category = await repository.UpdateAsync(id, data);

            if(category is null)
            {
                return new NotFoundResult();
            }

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Dellete(int id)
        {
            bool? flag = await repository.DeleteAsync(id);

            if(flag == false || flag is null)
            {
                return new NotFoundResult();
            }
            return Ok();
        }
    }
}
