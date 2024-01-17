using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
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
        public async Task<IActionResult> Get()
        {
            IEnumerable<Tag> tags = await repository.RetrieveAllAsync();

            return Ok(tags);
        }

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
        public async Task<IActionResult> Set([FromBody] Tag data)
        {
            Tag? tag = await repository.CreateAsync(data);
            if (tag is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(tag);
        }

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
