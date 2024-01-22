using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Repositories.Interfaces;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LikeController : ControllerBase
    {
        public readonly ILikeRepository repository;

        public LikeController(ILikeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Like> likes = await repository.RetrieveAllAsync();

            return Ok(likes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Like? like = await repository.RetrieveAsync(id);

            if (like is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(like);
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] Like data)
        {
            Like? like = await repository.CreateAsync(data);
            if (like is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(like);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Like data)
        {
            Like? like = await repository.UpdateAsync(id, data);

            if(like is null)
            {
                return new NoContentResult();
            }

            return Ok(like);
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
