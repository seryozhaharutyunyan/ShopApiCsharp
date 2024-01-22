using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        public readonly ICommentRepository repository;

        public CommentController(ICommentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Comment> comments = await repository.RetrieveAllAsync();

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Comment? comment = await repository.RetrieveAsync(id);

            if(comment is null)
            {
                return new NoContentResult();
            }

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] Comment data)
        {
            Comment? comment = await repository.CreateAsync(data);

            if(comment is null)
            {
                return new NoContentResult();
            }

            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Comment data)
        {
            Comment? comment = await repository.UpdateAsync(id, data);

            if(comment is null)
            {
                return new NoContentResult();
            }

            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool? flag = await repository.DeleteAsync(id);

            if(flag is null || flag == false)
            {
                return new NoContentResult();
            } 

            return Ok();
        }
    }
}
