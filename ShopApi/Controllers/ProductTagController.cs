using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTagController : ControllerBase
    {
        public readonly IProductTagRepository repository;

        public ProductTagController(IProductTagRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<ProductTag> productTags = await repository.RetrieveAllAsync();

            return Ok(productTags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ProductTag? productTag = await repository.RetrieveAsync(id);

            if (productTag is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(productTag);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Set([FromBody] ProductTag data)
        {
            ProductTag? productTag = await repository.CreateAsync(data);
            if (productTag is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(productTag);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductTag data)
        {
            ProductTag? productTag = await repository.UpdateAsync(id, data);

            if(productTag is null)
            {
                return new NoContentResult();
            }

            return Ok(productTag);
        }

        [Authorize(Roles = "admin")]
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
