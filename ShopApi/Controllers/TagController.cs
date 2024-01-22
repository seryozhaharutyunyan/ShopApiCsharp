using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        public readonly ITagRepository repository;

        public TagController(ITagRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Tag> tags = await repository.RetrieveAllAsync();

            return Ok(tags);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Tag? tag = await repository.RetrieveAsync(id);

            if (tag is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(tag);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Set([FromBody] Tag data)
        {
            Tag? tag = await repository.CreateAsync(data);
            if (tag is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(tag);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tag data)
        {
            Tag? tag = await repository.UpdateAsync(id, data);

            if(tag is null)
            {
                return new NoContentResult();
            }

            return Ok(tag);
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
